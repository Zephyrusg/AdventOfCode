$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay18"

function start-Expolsion{
    param(
        [char[]]
        $workArray,
        [int]
        $index,
        [string]
        $match
    )

    $length = $match.Length
    $numbers = $match -split(',')
    $leftnumber = [int]::parse($($numbers[0] -replace('\[',"")))
    $rightnumber = [int]::parse($($numbers[1] -replace('\]',"")))


    $workstring =  -join $workArray
    $workstring = $workstring.Remove($index,$length) #Remove pair
    $workstring = $workstring.Insert($index,'0')
    [char[]]$workArray = $workstring.ToCharArray()

    $checknumberleft =$false
    $checknumberright = $false

    :leftnumber for($i = $index -1; $i -gt 0;$i--){
        if($workArray[$i] -match '[0-9]'){
            $checknumberleft = $true
            $Firstnumberleft = [int]::Parse($workArray[$i])
            $FirstNumberLeftIndex = $i
            if($workarray[$($i-1)] -match '[0-9]'){
                $Firstnumberleft = [int]::Parse( -join $workarray[$($i-1)..$($i)])
                $FirstNumberLeftIndex--
            }
            break leftnumber
        }
    }
    :rightnumber for($i = $($index +1); $i -lt $workArray.Count;$i++){
        if($workArray[$i] -match '[0-9]'){
            $checknumberright = $true
            $firstnumberright = [int]::Parse($workArray[$i])
            $FirstNumberrightIndex = $i
            if($workarray[$($i+1)] -match '[0-9]'){
                $firstnumberright = [int]::Parse(-join $workarray[$i..$($i+1)])
             }
            break rightnumber
        }
    }


    if($checknumberright){
        $rightWorkingNumber = $rightnumber + $firstnumberright
        $rightWorkingNumber = [char[]]$($rightworkingnumber.ToString()).ToChararray()
        if($rightworkingnumber.Count -eq 1){
            $workArray[$FirstNumberRightIndex] = $rightWorkingNumber[0]
        }elseif($rightworkingnumber.count -eq 2){
            $workstring =  -join $workArray
            $length = $firstnumberright.ToString().Length
            $workstring = $workstring.Remove($($FirstNumberrightIndex),$length) #remove 1 or 2 numbers
            [string]$rightworkingnumber =  -join $rightworkingnumber
            $workstring = $workstring.Insert($($FirstNumberrightIndex),$rightworkingnumber)
            [char[]]$workArray = $workstring.ToCharArray()
        }
    }

    if($checknumberleft){
        $Leftworkingnumber = $leftnumber + $firstnumberleft + $extraleftnumber 
        $Leftworkingnumber = [char[]]$( $Leftworkingnumber.ToString()).ToChararray()
        if( $Leftworkingnumber.Count -eq 1){
            $workArray[$FirstNumberLeftIndex] =  $Leftworkingnumber[0]
        }elseif( $Leftworkingnumber.count -eq 2){
            $workstring =  -join $workArray
            $length = $Firstnumberleft.ToString().Length
            $workstring = $workstring.Remove($($FirstNumberLeftIndex),$length)
            [string] $Leftworkingnumber= -join  $Leftworkingnumber
            $workstring = $workstring.Insert($($FirstNumberLeftIndex), $Leftworkingnumber)
            [char[]]$workArray = $workstring.ToCharArray()           
        }
    }
   
    return $workArray
}


Function Find-Score{
    param(
      [string]  
      $finalstring  
    )
    $done = $false
    while(!$done){

     :again for($i=0;$i -lt $finalstring.length;$i++){
            if($finalstring[$i] -eq "["){
                $teststring =  $finalstring.Substring($i)
                If($teststring -match '^\[\d{1,5},\d{1,5}\]'){
                    $match = $Matches[0]
                    $length = $match.Length
                    $numbers = $match -split(',')
                    $left = [int]::parse($($numbers[0] -replace('\[',"")))
                    $right = [int]::parse($($numbers[1] -replace('\]',"")))
                    $subanswer = ($left*3)+($right*2)
                    $finalstring = $finalstring.Remove($i,$length)
                    [string]$subanswer = $subanswer.ToString()
                    $finalstring = $finalstring.Insert($i, $subanswer)
                    break again
                }
            }
        }
       
        if($finalstring -notmatch '\['){
            $done = $true
        }


    }
    return $finalstring
}

Function Start-split{
    param(
        [char[]]
        $workArray,
        $index,
        $match
    )

    $numbertospit = [int]::Parse($match)
    $rest = $numbertospit % 2
    $left = ($numbertospit - $rest) /2
    $right = ($numbertospit + $rest) /2
        
    $workstring = -join $workArray
    $workstring = $workstring.Remove($index ,2) #]
    $insertstring = "[$left,$right]"
    $workstring = $workstring.Insert($index,$insertstring)
    [char[]]$workArray = $workstring.ToCharArray()

    return $workArray

}

$calculations = 1
$currentstring = $data[0]
Write-Host "Starting with Snailfish Calculation: $($data.count) to go"
foreach($datastring in $data[1..$data.Count]){
    write-host "Doing Calculation:$calculations/$($data.count -1)"
    $currentstring = '[' + $currentstring + "," + $datastring + "]"
    [char[]]$workArray = $currentstring.ToCharArray() 
    $done = $false
    $noexplosionfound = $false
    while(!$done){
        $nestedlvl = -1
        $explosion = $false
        $split = $false
        :startagain for($i = 0; $i -lt $workArray.Length; $i++){
            Switch -regex ($workArray[$i]) {
                '\[' {
                    $nestedlvl++
                }
                '\]' {
                    $nestedlvl--
                }
                '[0-9]'{ 
                    $teststring = -join $workArray[$($i-1)..$($i+5)] 
                    $teststring2 = -join $workArray[$i..$($i+1)]
                    if($nestedlvl -ge 4 -and $teststring -match '^\[\d{1,2},\d{1,2}\]') 
                    {
                        $explosion = $true
                        $workArray = start-Expolsion -workArray $workArray -index $($i-1) -match $Matches[0]
                        break startagain
                    }elseif($noexplosionfound -and $teststring2 -match '^\d{2}$'){
                        $split = $true
                        $noexplosionfound = $false
                        $workArray = start-Split -workArray $workArray -index $i -match $Matches[0]
                        break startagain
                    }
                }
                ','{

                }
                default{}
            }
        }
        if(!$explosion -and !$split){
            if(!$noexplosionfound){
                $noexplosionfound = $true
            }else{
                $done = $true
            }
        }

    }
    $currentstring = -join $workArray
    $calculations++
}


Find-Score -finalstring  $currentstring