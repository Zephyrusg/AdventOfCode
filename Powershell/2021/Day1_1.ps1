$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday1_1"
$data = Get-Content -Path "$($path)\inputDay1_1"

[int]$increase = 0

for($i=1;$i -le $(($data.count)+1);$i++){

    if([int]$data[$i] -gt $([int]$data[$i-1])){
        $increase++
    }
}