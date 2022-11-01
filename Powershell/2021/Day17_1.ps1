$xMinTarget = 228 # not used
$XMaxTarget = 330 #  ot used
$yMinTarget = -96
$yMaxTarget = -50

$possibleY = [int[]]@()
$possibleY

:next for($i = 0; $i -lt 300; $i++ ){
    [int]$test = $i
    [int]$step = $i
    $totalSteps = 0
    while($test -gt $yMaxTarget){
        $step--
        $test += $step
        if($test -ge $yMinTarget -and $test -le $yMaxTarget){
            $possibleY += $i
            continue next
        }elseif($test -lt $yMinTarget){
            #Write-Host "Wrong Y: $i"
            continue next
        }
        $totalSteps++
    }
}

$maxValidY = ($possibleY | Measure-Object -Maximum).Maximum

$answer = ($maxValidY *($maxValidY  -1)/2)+ $maxValidY
$answer