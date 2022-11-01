$path = "C:\git\Scripts\AdventCode\2021"
$data = Get-Content -Path "$($path)\InputDay4_1" -Raw

#Parse Number to pick
[string]$numberdata = Get-Content -Path "$($path)\Input2Day4_1" 
[array]$temp = $numberdata.split(",")
[array]$numbersToPick = foreach($number in $temp) {([int]::parse($number))}

#parse Bingo Cards

$bingoCards = @()
$bingo = $false
$nl = [System.Environment]::NewLine
#Seperate all Bingocards
$items = ($data -split "$nl$nl")
Foreach($item in $items){
    #Spit Item in Rows
    $carddata = $item.Split("$nl")
    $card = [ordered]@{}
    $rownumber = 0
    foreach($row in $carddata){
        #ignore blank lines
        if($row -ne ""){
            [array]$temp = $row.split(" ")
            [array]$rowNumbers = @()
            foreach($number in $temp) {
                if($number){
                    $rowNumbers += ([int]::parse($number))
                }
            }
            $card.add($rownumber,$rowNumbers)
            $rownumber ++
        }

    }
    $bingoCards += $card
}

function mark-number{
    param(
        [Parameter(Mandatory)]
        $bingoCard,
        [Parameter(Mandatory)]
        [int]
        $drawnNumber

    )
    
    For($r=0;$r -lt $bingoCard.count; $r++){
        for($n=0; $n -lt ($bingoCard[$r]).count;$n++){
            if($bingoCard[$r][$n] -eq $drawnNumber){
                [string]$bingoCard[$r][$n] = "X"
            }
        }
    }

}

function confirm-Bingo {
    param (
        $bingoCard
    )

    $confirm = $false
    #check Row
    For($r=0;$r -lt $bingoCard.count; $r++){
        $count  = $bingoCard[$r] -match "X"
        if($count.count -eq 5){
            $confirm = $true
            break
        }

    }
    #check Column
    For($c=0;$c -lt $bingoCard.count;$c++){
        if($bingoCard[0][$c] -eq "X" -and $bingoCard[1][$c] -eq "X" -and $bingoCard[2][$c] -eq "X" -and $bingoCard[3][$c] -eq "X" -and $bingoCard[4][$c] -eq "X"){
            $confirm = $true
            break
        }
    }
    return $confirm
}
Foreach($drawnNumber in $numbersToPick){
    if($bingo){
        break
    }
    foreach($bingoCard in $bingoCards){
        Mark-Number -bingoCard $bingoCard -drawnNumber $drawnNumber   
    
        if($(confirm-Bingo -bingoCard $bingoCard)){
            #Write-host "Bingocard: $($bingoCard.values) Drawnumber: $drawnNumber"
            $bingo = $true
            $winnignumber = $drawnNumber
            $answercard = $bingoCard
            break
        }
    
    }    
}

[int]$answersum = 0
For($r=0;$r -lt $answercard.count;$r++){
    for($n=0;$n -lt $($answercard[$r]).count;$n++){
        if($answercard[$r][$n] -ne "X"){
            $answersum = $answersum + $($answercard[$r][$n])
        }
    }
}

$answer = $answersum * $winnignumber
write-host "Answer: $answer"