$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday1_1"
$data = Get-Content -Path "$($path)\inputDay1_1"

[int]$increase = 0

for($i=0;$i -le $(($data.count)+1);$i++){
    
    [int]$three = 0

    for($j=0;$j-lt 3; $j++){
        [int]$three += [int]$data[$($i+$j)]
    }
   
    if($three -gt $lastthree -and $i -gt 0){
        $increase++
    }
   
    $lastthree = $three
}
   