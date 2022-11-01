$xMinTarget = 288 
$xMaxTarget = 330 
$yMinTarget = -96
$yMaxTarget = -50

$possibleSolution = New-Object Collections.Generic.List[Array]

#Projection
for($x = 0; $x -le $xMaxTarget; $x++ ){
:next for($y = $yMinTarget;$y -le $yMinTarget;$y++){
        [int]$valueX = 0
        [int]$valueY = 0
        [int]$stepX = $x
        [int]$stepY = $y
        $totalSteps = 0
        while($valueY -ge $yMinTarget -and $valueX -le $xMaxTarget){
            
            $totalSteps++
            $valueX += $stepx
            $valueY += $stepY
            $stepY--
            if($stepX -gt 0){
                $stepX--
            }elseif($stepX-lt 0){
                $stepX++
            }
            if(($valueX -ge $xMinTarget -and $valueX -le $xMaxTarget) -and
            ($valueY -ge $yMinTarget -and $valueY -le $yMaxTarget)){
                $possibleSolution.Add(@($x,$y))
                continue next
            }
            
        }

    }
}


$answer = $possibleSolution.Count
$answer