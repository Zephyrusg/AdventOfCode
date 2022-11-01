$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay10"
#$data = Get-Content -Path "$($path)\Exampleday10"

$openchars = @('(','{','[','<')
$closedchars = @(')','}',']','>')
$correctpairs = @('()','{}','[]','<>')

$Hashpair = @{
    '(' = ')'
    '[' = ']'
    '<' = '>'
    '{' = '}' 
}


$correctendings = New-Object Collections.Generic.List[string[]]

Function Find-CorrectEnding{
    param(
        $listStilOpen,
        $Hashpair
    )
    [string[]]$correctending = @()
    for($i=0;$i -lt $listStilOpen.count;$i++){
        $correctending += $Hashpair[[string]$($listStilOpen[$i])]
    }
    return $correctending
}

foreach($item in $data){
    $currentOpenChars = New-Object Collections.Generic.List[string]
    $line = $item.ToCharArray()
    $valid = $true
    :loop Foreach($char in $line){
        if($openchars -contains $char){
            $currentOpenChars.Add($char)
        }
        elseif($closedchars -contains $char){
            $pair = $currentOpenChars[-1] + $char
            if($correctpairs -contains $pair){
                $currentOpenChars.RemoveAt($($currentOpenChars.Count) -1)
            }else{
                $valid = $false
                break loop
            }

        }
        
    }
    if($currentopenchars.Count -ne 0 -and $valid){
        $correctendings.Add($(Find-CorrectEnding -listStilOpen $currentOpenChars -Hashpair $Hashpair))
        
    }
}

[long]$score = 0
$scoreEndings = New-Object Collections.Generic.List[long]
foreach($ending in $correctendings){
    $i = $ending.count - 1
    [long]$score =  0
    for($($i = $ending.count - 1;);$i -ge 0;$i--){
        $score *= 5
        switch($ending[$i]){
            ')'{
                $score += 1
            }
            ']'{
                $score += 2
            }
            '}'{
                $score += 3
            }    
            '>'{
                $score += 4
            }
        }
        
    }
    $scoreEndings.Add($score)
}

$middle = ($scoreEndings.Count -1) /2
$scoresorted = ($scoreEndings | Sort-Object)
$scoresorted[$middle]
