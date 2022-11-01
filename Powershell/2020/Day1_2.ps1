$data = Get-Content -Path .\InputDay1_1
$solutionFound = $false

for($i=0; $i -lt $data.count; $i++){
    for($p=0; $p -lt $data.count; $p++){
        for($q=0; $q -lt $data.count; $q++){
            
            if($([int]$data[$i] + [int]$data[$p] + [int]$data[$q]) -eq 2020){
                $solution = [int]$data[$i] * [int]$data[$p] * [int]$data[$q]
                $solutionFound = $true
            }
            if($solutionFound){
                break
            }
        }
        if($solutionFound){
            break
        }
    }

    if($solutionFound){
        break
    }
}

Write-Host " i: $i value: $($data[$i])`
p: $p value: $($data[$p])`
q: $q value: $($data[$q])`
answer: $solution
"