$data = Get-Content -Path .\InputDay2_1
$acceptedpasswords = 0 
    
foreach($item in $data){
    [int]$min = $item.Split(" ")[0].Split("-")[0]
    [int]$max = $item.Split(" ")[0].Split("-")[1]
    $password = $item.Split(" ")[2]
    $requiredChar = $item.Split(" ")[1].ToCharArray()[0]
    $array = $password.ToCharArray()
    $count = 0
    foreach($char in $array){
        if($char -eq $requiredChar){
            $count++
        }
    }
    if(($count -ge $min) -and ($count -le $max)){
        $acceptedpasswords++
    }
}
