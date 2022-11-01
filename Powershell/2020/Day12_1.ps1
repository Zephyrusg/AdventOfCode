$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday12"
$data = Get-Content -Path "$($path)\inputDay12"

$VerbosePreference = "Continue"

$windrose = @{
    0 = 'N'
    90 = 'E'
    180 = 'S'
    270 = 'W' 
}



$direction = "E"
$winddegree = 90
$x = 0

$y =0
<#$north = 0 
$south = 0 
$east = 0 
$west = 0#> 

foreach($item in $data){

    $code = $item.Substring(0,1)
    [int]$number = ([int]::parse($($item.Substring(1))))

    if($code -eq 'F'){
        $code = $direction
    }

    switch($code){

        {$_ -eq 'L' -or $_ -eq 'R'}{
            if($code -eq 'R'){
                $winddegree  += $number
            }else{
                $winddegree  -= $number
            }

            if($winddegree  -lt 0){
                $winddegree += 360
            }elseif($winddegree  -ge 360){
                $winddegree -= 360
            }
            $direction = $windrose[$winddegree]

        }
        'N'{
            $y += $number
        }
        'S' {
            $y = $([Math]::abs($y - $number))
        }   
        'W'{
            $x = $([Math]::abs($x - $number))
        }
        'E'{
            $x = $([Math]::abs($x + $number))
        }

    }
}

$([Math]::abs($x)) + $([math]::Abs($y))