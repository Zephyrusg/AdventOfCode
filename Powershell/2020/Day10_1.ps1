$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#[int[]]$data = Get-Content -Path "$($path)\Exampleday10"
[int[]]$data = Get-Content -Path "$($path)\inputDay10"

$data += 0

[int[]]$sortedData = $data | Sort-Object 

$ones = 0
$twos = 0
$threes = 0

for($i=1;$i -lt $sortedData.count;$i++){
  
    [int]$jolt = $sortedData[$i] - $sortedData[$i-1]
    switch($jolt){
        
        1{
            $ones++
        }
        2{
            $twos++}

        3{
            $threes++
        }

    }
        
}

$threes++

write-host "ones: $ones Threes: $threes"
$answer = $ones * $threes
$answer