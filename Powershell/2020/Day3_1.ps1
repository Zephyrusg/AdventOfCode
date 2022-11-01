$data = Get-Content -Path .\InputDay3_1
[int]$steps = 0
[int]$tree = 0

foreach($line in $data){
    if($steps -gt 30){
        $steps = $steps - 31
    }
    if($line[$steps] -eq "#"){
        $tree++
    }
    $steps = $steps + 1
}