$data = Get-Content -Path .\InputDay1_1
$solutionFound = $false

for($i=0; $i -lt $data.count; $i++){
    for($p=0; $p -lt $data.count; $p++){
          
        if($([int]$data[$i] + [int]$data[$p]) -eq 2020){
            $solution = [int]$data[$i] * [int]$data[$p]
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

Write-Host " i: $i value: $($data[$i])`
p: $p value: $($data[$p])`
answer: $solution
"