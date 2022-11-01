$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay11"
#$data = Get-Content -Path "$($path)\Exampleday11"


$VerbosePreference = "SilentlyContinue"

$global:height = $data.Count
$global:width = $data[0].length

[System.Array] $matrix = [System.Array]::CreateInstance( [int32], $global:height,$global:width)

for($i=0;$i -lt $data.count;$i++){

    for($j=0;$j -lt $width;$j++){
      $matrix[$i,$j] =  ([int]::parse($data[$i][$j]))
    }
}

function Confirm-UlitimateFlash{
    param(
        $matrix
    )
    
    $done = $true

    :loop for($i=0;$i -lt $global:height;$i++){
        for($j=0;$j -lt $global:width;$j++){
            if($matrix[$i,$j] -ne 0){
                $done = $false
                break loop
            }   
        }
    }
    return $done
} 

function start-FlashDumbo{
    [CmdletBinding()]param(
        
        $matrix,
        $i,
        $j
    ) 

    $ExtraFlashpoints = New-Object Collections.Generic.List[array]
    $matrix[$i,$j] = 0
    
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

    foreach($neighbour in $neighbours){
        $x = $neighbour[0]
        $y = $neighbour[1]
        
        If($null -ne $matrix[$x,$y] -and $matrix[$x,$y] -ne 0 -and
        $x -ge 0 -and $y -ge 0){
            write-verbose "Upping: ($x,$y) from ($i,$j) $($matrix[$x,$y]) --> $($matrix[$x,$y] + 1)"
            $matrix[$x,$y]++
            if($matrix[$x,$y] -gt 9){
                $extraFlashpoint = @($x,$y)
                $extraFlashpoints.Add($extraFlashpoint)
                $matrix[$x,$y] = 0
            }
        }
    }
    #start extra Flashpoints
    foreach($extraFlashpoint in $extraFlashpoints){
        Write-Verbose "Start ExtraFlash on ($($extraflashpoint[0]),$($extraflashpoint[1]))"
        $countofFlashes += start-FlashDumbo -i $extraflashpoint[0] -j $extraflashpoint[1] -matrix $matrix
    }
}

#do every step
for($step = 0;!(Confirm-UlitimateFlash -matrix $matrix);$step++){
    
    #every Dumbo plus one
    Write-Verbose "Starting step: $step"
    $Flashpoints = New-Object Collections.Generic.List[array]
    for($i=0;$i -lt $data.count;$i++){
        for($j=0;$j -lt $width;$j++){
            $matrix[$i,$j]++
            if($matrix[$i,$j] -gt 9){
                $flashpoint = @($i,$j)
                $Flashpoints.Add($flashpoint)
                $matrix[$i,$j] = 0
            }   
        }
    }
    #start flashing
    foreach($flashpoint in $Flashpoints){
        write-verbose "Start Flash on ($($flashpoint[0]),$($flashpoint[1]))" 
        start-FlashDumbo -i $flashpoint[0] -j $flashpoint[1] -matrix $matrix 

    }
    Write-Verbose "End Step $step"

}

write-host "Step we looking for is $step"

