$data = Get-Content -Path .\InputDay2_1

$depth = 0
$forward = 0

for ($i = 0; $i -lt $Data.count; $i++) {
    $entry = $Data[$i]
    $splitResult = $entry.Split(" ")
    $direction = $splitResult[0]
    $number = $splitResult[1]

    switch($direction){

        forward{
            $forward = $forward + $number
        }
        down{
            $depth = $depth + $number
        }
        up{
            $depth = $depth - $number
        }

    }

}

$answer = $forward * $depth
Write-Host $answer