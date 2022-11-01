$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay20"
#$data = Get-Content -Path "$($path)\Exampleday20"
$decodestring = $data[0]

$dataimageinput = $data[2..$data.Count]


$global:height = $data.Count -2
$global:width = $data[2].Length

$times = 50


[System.Array] $imageinput = [System.Array]::CreateInstance( [string], $global:height,$global:width)

Function Get-DecodedPoint{
    param(
        [string[,]]
        $imageinput,
        [int]
        $i,
        [int]
        $j,
        [bool]
        $infinity
    )

    $neighbours = @(
        ($($i-1),$($j-1)), #upleft
        ($($i-1),$j), #up
        ($($i-1),$($j+1)), #upright
        ($i,$($j-1)), #left
        ($i,$j),
        ($i,$($j+1)), #right
        ($($i+1),$($j-1)), #downleft
        ($($i+1),$j), #down
        ($($i+1),$($j+1))  #downright
    )
    [string]$codeBinair = ""
    foreach($neighbour in $neighbours){
        $x = $neighbour[0]
        $y = $neighbour[1]
        if($x -lt 0 -or $y -lt 0 -or $x -ge $global:height -or $y -ge $global:width){
            if($infinity){
                $codeBinair += '1'
            }else{
                $codeBinair += '0'
            }
            
        }elseIf( $imageinput[$x,$y] -eq '#'){
        
            $codeBinair += '1'
        }else {$codeBinair += '0'}
    }

    $number = ([convert]::ToInt32($codeBinair,2))
    return $number
}


for($i=0;$i -lt $global:height;$i++){
    
    for($j=0;$j -lt $global:width;$j++){
        $imageinput[$i,$j] = $dataimageinput[$i][$j]
    }
}
$infinity = $false

Write-host "Starting Enhancing "
for($time =0; $time -lt $times; $time++){
    Write-host "Starting Enhancing $($time+1) /$times"
    [System.Array] $imageoutput = [System.Array]::CreateInstance( [string],$($global:height+2),$($global:width+2))

    for($i=0;$i -lt $global:height+2;$i++){
        for($j=0;$j -lt $global:width+2;$j++){
       
            $imageoutput[$i,$j] = '.'
        }
    }

    for($i=-1;$i -lt $($global:height+1);$i++){
        for($j=-1;$j -lt $($global:width+1);$j++){
            $decodenumber = Get-DecodedPoint -imageinput $imageinput -i $i -j $j -infinity $infinity
            $imageoutput[$($i+1),$($j+1)] = $decodestring[$decodenumber]
        }
    }
    $global:height = $global:height + 2
    $global:width = $global:width + 2
    $imageinput = $imageoutput
    if($infinity){
        $infinity = $false
    }else{
        $infinity = $true
    }
}

$countpixel = 0
for($i=0;$i -lt $global:height;$i++){
    for($j=0;$j -lt $global:width;$j++){
       if($imageinput[$i,$j] -eq '#'){
           $countpixel++
       }
    }
}

$countpixel


<#for($i=0;$i -lt $global:height;$i++){
    $line = ""
    for($j=0;$j -lt $global:width;$j++){
       $line += $imageinput[$i,$j]
       
           
       
    }
    $line
}#>