$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay13"
#$data = Get-Content -Path "$($path)\Exampleday13"



$instructions = ($data | Where-Object {$_ -like "Fold along*"}).Replace("fold along ", "")

[System.Collections.ArrayList]$points = ($data | Where-Object {$_ -notlike "Fold along*"})
$points.Removeat($($points.Count -1))

$startlayout = New-Object Collections.Generic.List[System.Collections.ArrayList]

$startlayout.Add($instructions[0].split('='))
$startlayout.Add($instructions[1].split('='))
foreach($item in $startlayout){
    if($item[0] -eq 'x'){
        $global:widght = $([int]$item[1] * 2) +1 
    }else{
        $global:height = $([int]$item[1] * 2) +1 
    }
}

$grid = New-Object Collections.Generic.List[System.Collections.ArrayList]

for($i = 0; $i -lt $global:height;$i++){
    $gridrow = [System.Collections.ArrayList]@(0) * $global:widght
    $grid.Add($gridrow)
}

foreach($point in $points){
    $x = $point.split(",")[0]
    $y = $point.split(",")[1]
    $grid[$y][$x] = '#'
}
Function New-FoldHorizontal{
    param(
        $grid,
        [int]
        $x
    )
    
    for($y=0;$y-lt $global:height;$y++){
        for($i= 1;$($x -$i) -ge 0;$i++){
            if(($grid[$y][$($x-$i)]) -eq '#' -or ($grid[$y][$($x+$i)] -eq '#' )){
                $grid[$y][$($x-$i)] = '#'
            }

        }
    }
    $global:widght = $x
    $newgrid = New-Object Collections.Generic.List[System.Collections.ArrayList]
    for($y=0;$y -lt $global:height;$y++){
        $grid[$y].RemoveRange($x,$global:widght)
        [System.Collections.ArrayList]$gridrow = $grid[$y] 
        $newgrid.Add($gridrow)
    }

    return $newgrid
}



Function New-FoldVertical{
     param(
        $grid,
        [int]
        $y
    )

    for($x=0;$x-lt $global:widght;$x++){
        for($i= 1;$($y - $i) -ge 0;$i++){
            if(($grid[$y-$i][$($x)]) -eq '#' -or ($grid[$y+$i][$($x)] -eq '#' )){
                $grid[$y-$i][$($x)] = '#'
            }

        }
    }
    $global:height = $y
    $newgrid = New-Object Collections.Generic.List[System.Collections.ArrayList]
    for($y=0;$y -lt $height;$y++){
        $gridrow =  $grid[$y].Clone()
        $newgrid.Add($gridrow)
    }

    return $newgrid
}

foreach($instruction in $instructions){
    $instruction = $instruction -split "="
    switch($instruction[0]){
        'x'{
            write-host "Starting Folding over X"
            $grid = New-FoldHorizontal -grid  $grid -x $instruction[1]
        }
        'y'{
            write-host "Starting Folding over Y"
            $grid = New-FoldVertical -grid $grid -y $instruction[1]
        }
    }
}


for($y=0;$y -lt $global:height;$y++){
    $printstring = ""
    for($x=0;$x -lt $global:widght;$x++){
        #Write-host "x: $x and y: $y"
        if($grid[$y][$x] -eq 0){
            $printstring += "."
        }else{
            $printstring += $grid[$y][$x]
        }
    }
    $printstring
}