$data = Get-Content -Path .\InputDay2_1
$acceptedpasswords = 0 
    
foreach($item in $data){
    [int]$numberOne = [int]($item.Split(" ")[0].Split("-")[0]) - 1
    [int]$numberTwo = [int]($item.Split(" ")[0].Split("-")[1]) - 1
    $password = $item.Split(" ")[2]
    $requiredChar = $item.Split(" ")[1].ToCharArray()[0]
  
    if((($password[$numberOne] -eq $requiredChar) -and ($password[$numberTwo] -ne $requiredChar)) -or `
    (($password[$numberTwo] -eq $requiredChar) -and ($password[$numberOne] -ne $requiredChar)) `
    ){
        $acceptedpasswords++
    }
}
