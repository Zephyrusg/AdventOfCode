$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday8_2"
$data = Get-Content -Path "$($path)\inputDay8"
#$data = Get-Content -Path "$($path)\Exampleday8"


$SeperatedData = New-Object 'System.Collections.Generic.List[array]'

foreach($dataLine in $Data) {

    #$dataLine = $dataLine.Replace('|','#')
    $signalResultSplit = $dataLine -split ' | ',0,"SimpleMatch"

    $newSignalData = $signalResultSplit[0].Split(" ")

    $sortedNewSignalData = $newSignalData | ForEach-Object {[string] -join ($_.ToCharArray() | Sort-Object)}

    $newOutputData = $signalResultSplit[1].Split(" ")

    $sortedNewOutputData = $newOutputData | ForEach-Object {[string] -join ($_.ToCharArray() | Sort-Object)}

    $SeperatedDataLine = New-Object -TypeName string[][] 2

    $SeperatedDataLine[0] = $sortedNewSignalData

    $SeperatedDataLine[1] = $sortedNewOutputData

    $SeperatedData.Add($SeperatedDataLine)

}

function Decode-Output{
    param(
       [hashtable]
       $decodedTable,

       [string[]]
       $EncodedStrings
    )

    [string]$decodedOutput = ""
    
    foreach($encodedString in $encodedStrings){
        [string]$decodedOutput += [string]$decodedTable[$encodedString]
    }
    [int]$number = $decodedOutput
    
    return $number

}

function Decode-Number{
    param(
        [string[]]
        $encodedNumbers
    )
    $decodesNumbers = @{}

    $obj = [pscustomobject]@{
        zero = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        one = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        two = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        three = @{
            decoded = [bool]$false
            decodedata = [string]""

            }        
        four = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        five = @{
            decoded = [bool]$false
            decodedata = [string]""

            }        
        six = @{
            decoded = [bool]$false
            decodedata = [string]""
            
        }
        seven = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        eight = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        nine = @{
            decoded = [bool]$false
            decodedata = [string]""

            }
        allTen = [int]0
        #a = "" Inrelevant note
        #b = ""
        c = ""
        #d = ""
        #e = ""
        f = ""
        #g = ""
    }

    while($obj.allTen -ne 10){
        
        foreach($encodednumber in $encodedNumbers){
            switch($encodednumber.length){
                
                2{
                    if(!$obj.one.decoded){
                        $obj.one.decodedata = $encodednumber
                        $obj.one.decoded = $true
                        $obj.allTen++
                    }
                }
                3{
                    if(!$obj.seven.decoded){
                        $obj.seven.decodedata = $encodednumber
                        $obj.seven.decoded = $true
                        $obj.allTen++
               
                    }
                }
                4{
                    if(!$obj.four.decoded){
                        $obj.four.decodedata = $encodednumber
                        $obj.four.decoded = $true
                        $obj.allTen++
                    }
                }
                
                7{
                    if(!$obj.eight.decoded){
                        $obj.eight.decodedata = $encodednumber
                        $obj.eight.decoded = $true
                        $obj.allTen++
                    }
                }
                
                6{
                    #check six
                    if(!$obj.six.decoded -and ($obj.one.decoded)){
                        if($encodednumber -notmatch $($obj.one.decodedata).ToChararray()[0] -or 
                            $encodednumber -notmatch $($obj.one.decodedata).ToChararray()[1]){
                            $obj.six.decodedata = $encodednumber
                            $obj.six.decoded = $true
                            $obj.allTen++
                            if($encodednumber -match $($obj.one.decodedata).ToChararray()[0]){
                                $obj.f = $($obj.one.decodedata).ToChararray()[0]
                                $obj.c = $($obj.one.decodedata).ToChararray()[1]
                            }else{
                                $obj.c = $($obj.one.decodedata).ToChararray()[0]
                                $obj.f = $($obj.one.decodedata).ToChararray()[1]
                            }
                        }
                    }elseif($obj.six.decoded){
                        #check for Zero
                        if(!$obj.zero.decoded -and $obj.four.decoded -and $encodednumber -ne $obj.six.decodedata){
                            foreach($char in $obj.four.decodedata.ToChararray()){
                                #Write-Host "this is $char"
                                if($encodednumber -notmatch $char){
                                    $obj.zero.decodedata = $encodednumber
                                    $obj.zero.decoded = $true
                                    $obj.allTen++
                                    break
                                }
                            }
                        }elseif(!$obj.nine.decoded -and $obj.six.decoded -and $obj.zero.decoded -and
                        $encodednumber -ne $obj.six.decodedata -and 
                        $encodednumber -ne $obj.zero.decodedata){
                            $obj.nine.decodedata = $encodednumber
                            $obj.nine.decoded = $true
                            $obj.allTen++
                        }
                    }
                }
                5{
                    if($obj.six.decoded){
                        if(!$obj.two.decoded){
                            if($encodednumber -notmatch $obj.f){    
                                $obj.two.decodedata = $encodednumber
                                $obj.two.decoded = $true
                                $obj.allTen++
                            }
                        }
                        elseif(!$obj.Five.decoded){
                            if($encodednumber -notmatch $obj.c){
                                $obj.five.decodedata = $encodednumber
                                $obj.five.decoded = $true
                                $obj.allTen++
                            }
                        }
                        elseif(!$obj.three.decoded -and $obj.two.decoded -and $obj.five.decoded -and
                            $encodednumber -ne $obj.two.decodedata -and 
                            $encodednumber -ne $obj.five.decodedata){
                                $obj.three.decodedata = $encodednumber
                                $obj.three.decoded = $true
                                $obj.allTen++
        
                        }
                    }
                }
           }
        }
        
    }
    $decodesNumbers.Add($obj.zero.decodedata,0)
    $decodesNumbers.Add($obj.one.decodedata,1)
    $decodesNumbers.Add($obj.two.decodedata,2)
    $decodesNumbers.Add($obj.three.decodedata,3)
    $decodesNumbers.Add($obj.four.decodedata,4)
    $decodesNumbers.Add($obj.five.decodedata,5)
    $decodesNumbers.Add($obj.six.decodedata,6)
    $decodesNumbers.Add($obj.seven.decodedata,7)
    $decodesNumbers.Add($obj.eight.decodedata,8)
    $decodesNumbers.Add($obj.nine.decodedata,9)

    return $decodesNumbers
}

$i = 1
$count = 0
foreach($line in $SeperatedData){
    #Write-Host "i: $i"
    $decodedTable = Decode-Number -encodedNumbers $line[0]
    $newnumber = Decode-Output -decodedTable $decodedTable -EncodedStrings $line[1]
    $i++
    #Write-Host "newnumber: $newnumber"
    $count += $newnumber
    
}#>

$count