$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday25"
$data = Get-Content -Path "$($path)\inputDay25"


$map = New-Object Collections.Generic.List[array]
$tempmap = New-Object Collections.Generic.List[array]
$previousmap = New-Object Collections.Generic.List[array]
foreach($item in $data){
    $mapline = $item.ToCharArray()
    $map.Add($mapline)
}
$global:height = $map.Count
$global:width = $map[0].Count

for($i=0;$i -lt $global:height;$i++){

    $mapline = $map[$i].clone()
    $tempmap.Add($mapline)
    $previousmap.Add($mapline)
}

Function print-map{
    param(
        $map
    )

    for($i=0;$i -lt $global:height;$i++){
        $line = ""
        for($j=0;$j -lt $global:width;$j++){
        [string]$line += $map[$i][$j]
        
            
        
        }
        $line
    }
}

$done = $false
$rounds = 0
while(!$done){
    $rounds++
    Write-host "Starting Moving Round: $($rounds)"

    foreach($status in @('east','south')){
        
        $map = $tempmap
        $tempMap = New-Object Collections.Generic.List[array] 
        for($i=0;$i -lt $global:height;$i++){
            #$mapline = New-Object char[] $global:width
            #$mapline = @(0) * $global:width
            $mapline = $map[$i].clone()
            $tempmap.Add($mapline)
        }
        for($i=0;$i -lt $global:height;$i++){
            for($j=0;$j -lt $global:width;$j++){
                $char = [char]$map[$i][$j]
                
                switch($char){
                    {$_ -eq '>' -and $status -eq "east"}
                    {
                        $neighbour = @($i,$($j+1))
                        if($j -eq $($global:width - 1)){
                            $neighbour[1] = 0
                    
                        }

                        if($map[$($neighbour[0])][$($neighbour[1])] -eq '.'){
                            $tempmap[$($neighbour[0])][$($neighbour[1])] = $char
                            $tempmap[$i][$j] = '.'

                        }

                    }
                    {$_ -eq 'V' -and $status -eq "south"}
                    {
                        $neighbour = @($($i+1),$j)
                        if($i -eq $($global:height - 1)){
                            $neighbour[0] = 0
                    
                        }

                        if($map[$($neighbour[0])][$($neighbour[1])] -eq '.'){
                                $tempmap[$($neighbour[0])][$($neighbour[1])] = $char
                                $tempmap[$i][$j] = '.'
                               
                        }
                    }
                }
                
            }
        }
        
    }
    #print-map -map $map
    if(!(Compare-Object -ReferenceObject $tempmap -DifferenceObject $previousmap)){
        $done = $true
        
    }else{
        #$map = $tempmap
        #$tempMap = New-Object Collections.Generic.List[array]
        $previousmap = New-Object Collections.Generic.List[array]
        for($i=0;$i -lt $global:height;$i++){
            $mapline = $tempmap[$i].clone()
            #$tempmap.Add($mapline)
            $previousmap.Add($mapline)
        }
    }
    
}

$rounds

