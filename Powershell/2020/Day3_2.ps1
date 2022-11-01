function Count-Trees([int]$right, [int]$down){
    $data = Get-Content -Path .\InputDay3_1
    [int]$steps = 0
    [int]$tree = 0

    for($i=0;$i -lt $data.count; $i = $i + $down){
        $line = $data[$i]
        if($steps -gt 30){
            $steps = $steps - 31
        }

        if($line[$steps] -eq "#"){
            $tree++
        }
        $steps = $steps + $right
    }

    return $tree
}

$trees1 = Count-Trees -right 1 -down 1
$trees2 = Count-Trees -right 3 -down 1
$trees3 = Count-Trees -right 5 -down 1
$trees4 = Count-Trees -right 7 -down 1
$trees5 = Count-Trees -right 1 -down 2

$answer = $trees1 * $trees2 * $trees3 * $trees4 * $trees5