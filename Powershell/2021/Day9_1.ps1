$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay9"
#$data = Get-Content -Path "$($path)\Exampleday9"
#$data = Get-Content -Path "$($path)\test"

$global:height = $data.Count
$global:width = $data[0].length

[System.Array] $matrix = [System.Array]::CreateInstance( [int32], $global:height,$global:width)

for($i=0;$i -lt $data.count;$i++){

    for($j=0;$j -lt $width;$j++){
      $matrix[$i,$j] =  ([int]::parse($data[$i][$j]))
    }
}


$counter = 0

for($i=0;$i -lt $data.count;$i++){

    for($j=0;$j -lt $width;$j++){
        $pos = $matrix[$i,$j]
        $up =  $matrix[$($i-1),$j]
        $down = $matrix[$($i+1),$j]
        $left = $matrix[$i,$($j-1)]
        $right =$matrix[$i,$($j+1)]
        If($($i-1) -lt 0){$up = 10}
        if($i -eq $global:height - 1){$down = 10}
        if($($j-1) -lt 0) {$left = 10}
        if($j -eq $global:width - 1){$right = 10}

        if(($pos -lt $up) -and ($pos -lt $down) -and ($pos -lt $left) -and ($pos -lt $right)){
            
            
            $counter += $pos + 1
            
            
        }


    }
}

$counter

