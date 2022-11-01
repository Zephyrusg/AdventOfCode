$p1Location = 8
$p2location = 6

$p1Score = 0
$P2Score = 0

$p1turn = $true
$p2turn = $true 

$dicerolled = 0

$rollA = 1
$rollB = 2
$rollC = 3
$nextTotalRoll = 6

do{

    If($p1turn){
        
        $p1Location = $($p1Location + $nextTotalRoll) % 10
        If($p1Location -eq 0){$p1Location = 10}
        $p1Score += $p1Location

    }else{
        $p2Location = $($p2Location + $nextTotalRoll) % 10
        If($p2Location -eq 0){$p2Location = 10}
        $p2Score += $p2Location

    }


    $rollA = ($rollA + 3) % 100 
    $rollB = ($rollB + 3) % 100 
    $rollC = ($rollC + 3) % 100 
    

    If($rollA -eq 0){$rollA = 100}
    If($rollB -eq 0){$rollB = 100}
    If($rollC-eq 0){$rollC = 100}
    $nextTotalRoll = $rollA + $rollB + $rollC

    $dicerolled += 3

    if($p1turn){
        $p1turn = $false
        $p2turn = $true
    }elseif($p2turn) {
        $p2turn = $false
        $p1turn = $true
    }

}while($p1score -lt 1000 -and $p2score -lt 1000 )

if($p1Score -ge 1000 ){
    write-host "Loser P2, Score: $p2score Dice rolled: $dicerolled"
    $P2Score * $dicerolled
}else{
    write-host "Loser P1, Score: $p1score Dice rolled: $dicerolled"
    $p1Score * $dicerolled
}