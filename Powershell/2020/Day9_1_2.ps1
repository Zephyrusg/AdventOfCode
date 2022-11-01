$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2020"
#[int[]]$data = Get-Content -Path "$($path)\Exampleday9"
[int64[]]$data = Get-Content -Path "$($path)\inputDay9"
$ErrorActionPreference = "stop"
#$preamble = 5 #example
$preamble = 25 #inputdata

function Confirm-number{
    param(
        [int64[]]
        $Subset,
        [int64]$number    
    )

    for($i=0;$i -lt $Subset.Count;$i++){
        for($j=0;$j -lt $Subset.Count;$j++){
            if($i -ne $j){
                if(($subset[$i] + $Subset[$j]) -eq $number -and ($subset[$i] -ne $Subset[$j])){
                    return $true
                }
            }
        }
        
    }
    return $false
}

function Find-SpecialSubset{
    param(
        [int64[]]
        $data,
        [int64]$number    
    )
   # :loop while($true){
    :loop for($i=0;$i -lt $data.Count;$i++){
        [int64]$sum =0
        for($j=$i;$j -lt $data.Count;$j++){
            $sum += $data[$j]  
                
                if($sum -gt $number){
                    break
                }
                if($sum -eq $number){
                    break loop
                }
        }
        
    }
   # }
    [int64[]]$subSet = $data[$i..$j]
    return $subSet
}


For($x=$preamble;$x -lt $data.count;$x++){
    [int64[]]$subset = $data[$($x-$preamble)..$($x-1)]
    
    [bool]$check = Confirm-number -Subset $subset -number $data[$x]
    if(!$check){
        break
    }
}

write-host "Answer part one: $($data[$x])"
[int64[]]$subSet = Find-SpecialSubset -data $data -number $data[$x]
$answerSet = $subSet | Sort-Object
[int]$answer = $answerSet[0] + $answerSet[-1]
write-host "Answer part two: $answer"