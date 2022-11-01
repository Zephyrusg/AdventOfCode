$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
$data = Get-Content -Path "$($path)\Exampleday11"
#$data = Get-Content -Path "$($path)\inputDay11"


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
        ($($i-1),$j), #up
        ($($i+1),$j), #down
        ($i,$($j-1)), #left
        ($i,$($j+1)), #right
        ($($i-1),$($j-1)), #upleft
        ($($i-1),$($j+1)), #upright
        ($($i+1),$($j-1)), #downleft
        ($($i+1),$($j+1))  #downright
    )
    
    $Allseatsempty = $true
    $occupiedSeats = 0

    :loop foreach($neighbour in $neighbours){
        $x = $neighbour[0]
        $y = $neighbour[1]
        
        If($x -lt $global:height -and $y -lt $global:width -and
        $x -ge 0 -and $y -ge 0){
            if($map[$x][$y] -eq "#"){
                Write-Verbose "Ocuppied Seat x: $x y: $y"
                if($char -eq [char]'L'){
                    $Allseatsempty = $false
                    break loop
                }
                $occupiedSeats++
                if($char -eq [char]'#' -and $occupiedSeats -ge 4){

                }
            }elseif($map[$x][$y] -eq "L"){
                Write-Verbose "Empty Seat x: $x y: $y"
                
            }else{
                Write-Verbose "Floorspace x: $x y: $y"
            }
        }
    }
    switch($char){
        '#'{
            if($occupiedSeats -ge 4){
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
$rounds = 0
while(!$done){
    Write-Verbose "Strating new Round"

    for($i=0;$i -lt $global:height;$i++){
        for($j=0;$j -lt $global:width;$j++){
            $char = [char]$map[$i][$j]
            if($char -eq [char]'.'){
                $tempmap[$i][$j] = [char]'.'
            }else{
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
    write-host "Starting with Round: $rounds"
}

Get-OccupySeatcount -map $map

Get-Date