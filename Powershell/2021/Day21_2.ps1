
$global:t = [bigint[]]@(0,0)


$global:combinations = @{}

foreach( $r1 in @(1..3)){
    foreach($r2 in @(1..3)){
        foreach($r3  in @(1..3)){
            $value = $r1 + $r2 + $r3
            if($combinations.keys -contains $value){
                $combinations[$value] ++
            }else{
                $combinations.Add($value,1)
            }

        }
    }

}
function New-Move($p1,$p2,$sc1=0,$sc2=0,$turn="Player1",$combi){
    
    foreach( $roll in @(9..3)){
    
        if($turn -eq "player1"){
            $np1 = ($p1+$roll -1) % 10 +1
            $nsc1 = $sc1 + $np1
            if ($nsc1 -ge 21)
            {
                $newcombi = $combi * $global:combinations[$roll]
                $global:t[0] += $newcombi
            }
            
            else{
                $newcombi = $combi * $global:combinations[$roll]
                New-Move -p1 $np1 -p2 $p2 -sc1 $nsc1 -sc2 $sc2 -turn "Player2" -combi $newcombi
            }
        }
        else{
            $np2 = ($p2+$roll- 1) % 10 +1
            $nsc2 = $sc2 + $np2
        
            if($nsc2 -ge 21)
            {
                $newcombi = $combi * $global:combinations[$roll]
                $global:t[1] += $newcombi
            }  
            
            else{
                $newcombi = $combi * $global:combinations[$roll]
                New-Move -p1 $p1 -p2 $np2 -sc1 $sc1 -sc2 $nsc2 -turn "Player1" -combi $newcombi
                
            }
        }
        
    }
    
}

New-move -p1 8 -p2 6 -turn "player1" -combi 1

$t

