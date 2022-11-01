$data = Get-Content -Path .\InputDay2_2

[int]$depth = 0
[int]$forward = 0
[int]$aim = 0

for ($i = 0; $i -lt $Data.count; $i++) {
    $entry = $Data[$i]
    $splitResult = $entry.Split(" ")
    [string]$direction = $splitResult[0]
    [int]$number = $splitResult[1]

    switch($direction){

        forward{
            $forward = $forward + $number
            $depth = $depth + $($number * $aim)
        }
        down{
            #$depth = $depth + $number
            $aim = $aim + $number
        }
        up{
            #$depth = $depth - $number
            $aim = $aim - $number
        }

    }

}

$answer = $forward * $depth
Write-Host $answer