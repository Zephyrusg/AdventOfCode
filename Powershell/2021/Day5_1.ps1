$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday5_1"
$data = Get-Content -Path "$($path)\inputDay5_1"

$sizematrix = 1000

[System.Array] $matrix = [System.Array]::CreateInstance( [int32], $sizematrix,$sizematrix) 

$ParsedInput = @()
foreach($item in $data){
    $parsedItem = ($item -replace ' -> ',',').Split(',')

    $line = New-Object -TypeName PSCustomObject @{
        x1 = ([int]::parse($parsedItem[0]))
        y1 = ([int]::parse($parsedItem[1]))
        x2 = ([int]::parse($parsedItem[2]))
        y2 = ([int]::parse($parsedItem[3]))
    }
    $ParsedInput += $line
}

foreach($line in $ParsedInput){
    switch($line){
        ({$line.x1 -eq $line.x2}){
            $x = $line.x1
            $steps = @($($line.y1)..$($line.y2))
            Write-Verbose "x1:$($line.x1) and x2:$($line.x2) and Steps: $steps"
            foreach($step in $steps){
                $matrix[$step,$x] ++
            }
        }
        ({$line.y1 -eq $line.y2}){
            $y = $line.y1
            $steps = @($($line.x1)..$($line.x2))
            Write-Verbose "y1:$($line.y1) and y2:$($line.y2) and Steps: $steps"
            foreach($step in $steps){
                $matrix[$y,$step] ++
            }
        }
        ({[Math]::Abs($line.y1 - $line.y2) -eq [Math]::Abs($line.x1 - $line.x2)}){
            #write-host "Diagonal"
            [int]$x1 = $line.x1
            [int]$x2 = $line.x2
            [int]$y1 = $line.y1
            [int]$y2 = $line.y2

            #down -> right
            if($x2 -gt $x1 -and $y2 -gt $y1){
                while($x1 -le $x2){
                    #Write-Host "x: $x1 y1 = $y1"
                    $matrix[$y1,$x1] ++
                    $y1 ++
                    $x1 ++
                }
            }
            #down -> left
            elseif($x1 -gt $x2 -and $y2 -gt $y1){
                while($x1 -ge $x2){
                    #Write-Host "x: $x1 y1 = $y1"
                    $matrix[$y1,$x1] ++
                    $y1 ++
                    $x1 --
                }
            }
            #up -> right
            elseif($x2 -gt $x1 -and $y1 -gt $y2){
                while($x1 -le $x2){
                    #Write-Host "x: $x1 y1 = $y1"
                    $matrix[$y1,$x1] ++
                    $y1 --
                    $x1 ++
                }
            }
            #up -> left
            elseif($x1 -gt $x2 -and $y1 -gt $y2){
                while($x1 -ge $x2){
                    #Write-Host "x: $x1 y1 = $y1"
                    $matrix[$y1,$x1] ++
                    $y1 --
                    $x1--
                }
            }
        }
        default{
            #Write-Host "Not a direct line."
        }
    }
}

$mutipleLines = 0

For($x=0;$x -lt $sizematrix;$x++){
    for($y=0;$y -lt $sizematrix;$y++){
        if($matrix[$y,$x] -ge 2){
            $mutipleLines ++
        }
    }
}

Write-Host $mutipleLines