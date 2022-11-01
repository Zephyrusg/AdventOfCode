$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay9"
#$data = Get-Content -Path "$($path)\Exampleday9"
#$data = Get-Content -Path "$($path)\test"

$global:height = $data.Count
$global:width = $data[0].length

[System.Array] $matrix = [System.Array]::CreateInstance( [int32], $global:height,$global:width)

for($i=0;$i -lt $data.count;$i++){

    for($j=0;$j -lt $width;$j++){
      $matrix[$i,$j] =  ([int]::parse($data[$i][$j]))
    }
}

function get-Basinpath{
    param(
        $matrix,
        $tempmatrix,
        $i,
        $j
    ) 
    $tempmatrix[$i,$j]++
    #what is up down left right
    $pos = $matrix[$i,$j]
    $up =  $matrix[$($i-1),$j]
    $down = $matrix[$($i+1),$j]
    $left = $matrix[$i,$($j-1)]
    $right =$matrix[$i,$($j+1)]

    #is there an offide somewhere
    If($i-1 -lt 0){$up = -1}
    if($i -eq $global:height-1){$down = -1}
    if($j-1 -lt 0) {$left = -1}
    if($j -eq $global:width - 1){$right = -1}

    #flow found run function
    if($up -gt $($pos) -and $up -ne 9){
        get-Basinpath -matrix $matrix -tempmatrix $tempmatrix -i $($i-1) -j $j 
    }
    if($down -gt $($pos) -and $down -ne 9){
        get-Basinpath -matrix $matrix -tempmatrix $tempmatrix -i $($i+1) -j $j 
    }
    if($left -gt $($pos) -and $left -ne 9){
        get-Basinpath -matrix $matrix -tempmatrix $tempmatrix -i $i -j ($j-1) 
    }
    if($right -gt $($pos) -and $right -ne 9){
        get-Basinpath -matrix $matrix -tempmatrix $tempmatrix -i $i -j ($j+1) 
    }

}

[array]$arrayofbasins =  (0)


for($i=0;$i -lt $data.count;$i++){

    for($j=0;$j -lt $width;$j++){
        $pos = $matrix[$i,$j]
        $up =  $matrix[$($i-1),$j]
        $down = $matrix[$($i+1),$j]
        $left = $matrix[$i,$($j-1)]
        $right =$matrix[$i,$($j+1)]
        If($($i-1) -lt 0){$up = 10}
        if($i -eq $global:height - 1){$down = 10}
        if($($j-1) -lt 0) {$left = 10}
        if($j -eq $global:width - 1){$right = 10}

        if(($pos -lt $up) -and ($pos -lt $down) -and ($pos -lt $left) -and ($pos -lt $right)){
            [System.Array] $tempmatrix = [System.Array]::CreateInstance( [int32], $global:height,$global:width)
            get-Basinpath -matrix $matrix -tempmatrix $tempmatrix -i $i -j $j 
            $counter = 0
            for($x=0;$x -lt $global:height;$x++){
                for($y=0; $y -lt $global:width;$y++){
                    if($tempmatrix[$x,$y] -gt 0){
                        $counter++
                    }
                }
            }
            $arrayofbasins += $counter
            
        }


    }
}

$highestbasins = ($arrayofbasins | Sort-Object)[$(-3)..$(-1)]
$answer = 1
foreach($basin in $highestbasins){
    $answer = $answer * $basin
}

$answer

