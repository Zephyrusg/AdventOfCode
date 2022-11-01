$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday11"
$data = Get-Content -Path "$($path)\inputDay11"

$map = New-Object Collections.Generic.List[array]
#$map = [array]@()
$tempmap = New-Object Collections.Generic.List[array]
foreach($item in $data){
    $mapline = $item.ToCharArray()
    $map.Add($mapline)
    #$map += $mapline
}
$global:height = $map.Count
$global:width = $map[0].Count

for($i=0;$i -lt $global:height;$i++){
    #$mapline = New-Object char[] $global:width
    #$mapline = @(0) * $global:width
    $mapline = $map[$i].clone()
    $tempmap.Add($mapline)
}

#$tempmap = $map.Clone()
Get-Date 


Function Get-OccupySeatcount{
    param(
        $map
    )

    $counter = 0 
    for($i=0;$i -lt $global:height;$i++){
        for($j=0;$j -lt $global:width;$j++){
        
            if($map[$i][$j] -eq '#'){
                $counter++
            }
        }
    }
    return $counter
}

function Find-SeatinDirection{
    param(
        [ValidateSet("up","down","left","right","upLeft","upRight","downLeft","downright")]
        [string]
        $direction,
        $map,
        $i,
        $j
    )

    switch($direction){
        'up'{
            for($x= $i - 1;$x -ge 0;$x--){
                if($map[$x][$j] -eq [char]'#' -or $map[$x][$j] -eq [char]'L'){
                    return [char]$($map[$x][$j])
                }
            }
        }
        'down'{
            for($x= $i + 1;$x -lt $global:height;$x++){
                if($map[$x][$j] -eq [char]'#' -or $map[$x][$j] -eq [char]'L'){
                    return [char]$($map[$x][$j])
                }
            }
        }
        'left'{
            for($x= $j - 1;$x -ge 0;$x--){
                if($map[$i][$x] -eq [char]'#' -or $map[$i][$x] -eq [char]'L'){
                    return [char]$($map[$i][$x])
                }
            }
        }
        'right'{
            for($x= $j + 1;$x -lt $global:width;$x++){
                if($map[$i][$x] -eq [char]'#' -or $map[$i][$x] -eq [char]'L'){
                    return [char]$($map[$i][$x])
                }
            }
        }
        'upLeft'{
            $j = $j - 1
            for($x= $i - 1;($x -ge 0 -and $j -ge 0);$x--){
                if($map[$x][$j] -eq [char]'#' -or $map[$x][$j] -eq [char]'L'){
                    return [char]$($map[$x][$j])
                }
                $j--
            }
        }
        'upRight'{
            $j = $j + 1
            for($x= $i - 1;($x -ge 0 -and $j -lt $global:width);$x--){
                if($map[$x][$j] -eq [char]'#' -or $map[$x][$j] -eq [char]'L'){
                    return [char]$($map[$x][$j])
                }
                $j++
            }
        }
        'downLeft'{
            $j = $j - 1
            for($x= $i + 1;($x -lt $global:height -and $j -ge 0);$x++){
                if($map[$x][$j] -eq [char]'#' -or $map[$x][$j] -eq [char]'L'){
                    return [char]$($map[$x][$j])
                }
                $j--
            }
        }
        'downRight'{
            $j = $j + 1
            for($x= $i + 1;($x -lt $global:height -and $j -lt $global:width);$x++){
                if($map[$x][$j] -eq [char]'#' -or $map[$x][$j] -eq [char]'L'){
                    return [char]$($map[$x][$j])
                }
                $j++
            }
        }
    }
    return [char]'.'
}

Function Set-seat{
    [CmdletBinding()]param(   
        $map,
        $tempmap,
        [int]
        $i,
        [int]
        $j,
        [char]
        $char

    ) 

    $neighbours = @(
        "up","down","left","right",
        "upLeft","upRight","downLeft","downright"
    )
    
    $Allseatsempty = $true
    $occupiedSeats = 0

    :loop foreach($neighbour in $neighbours){
        
       
        $SeatinDirection = Find-SeatinDirection -i $i -j $j -direction $neighbour -map $map
        
        if($SeatinDirection -eq [char]'#'){
            Write-Verbose "Ocuppied Seat x: $x y: $y"
            if($char -eq [char]'L'){
                $Allseatsempty = $false
                break loop
            }
            $occupiedSeats++
            if($occupiedSeats -ge 5){
                break loop
            }
        }
  
    }
    switch($char){
        '#'{
            if($occupiedSeats -ge 5){
                $tempmap[$i][$j] = [char]'L'
            }else{
                $tempmap[$i][$j] = [char]'#'
            }
        }
        'L'{
            if($Allseatsempty){
                $tempmap[$i][$j] = [char]'#'
            }else{
                $tempmap[$i][$j] = [char]'L'
            }
        }
        default{

        }
    }

}

$done = $false
$rounds = 1
while(!$done){
    write-host "Starting with Round: $rounds"

    for($i=0;$i -lt $global:height;$i++){
        for($j=0;$j -lt $global:width;$j++){
            $char = [char]$map[$i][$j]
            if($char -eq [char]'#' -or $char -eq [char]'L'){
                Set-seat -i $i -j $j -map $map -tempmap $tempmap -char $char
            }
        }
    }

    if(!(Compare-Object -ReferenceObject $map -DifferenceObject $tempmap)){
        $done = $true
    }else{
        $map = $tempmap
        $tempMap = New-Object Collections.Generic.List[array] 
        for($i=0;$i -lt $global:height;$i++){
            #$mapline = New-Object char[] $global:width
            #$mapline = @(0) * $global:width
            $mapline = $map[$i].clone()
            $tempmap.Add($mapline)
            #$tempmap = $map.Clone()
        }
    }
    $rounds++
}

Get-OccupySeatcount -map $map

Get-Date