$path = "C:\Git\Scripts\AdventCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday5_1"
$data = Get-Content -Path "$($path)\inputDay5_1"

function Calculate-Number {
    Param(
    [Parameter(Mandatory)]
    [ValidateSet("Row", "Column")]
    [string]
    $kind,
    $code
    )
    if($kind -eq "Row"){
        $range = @(0..127)
    }else{
        $range = @(0..7)
    }    
    for($x=0;$x -lt $($code.length);$x++){
        $letter = $code[$x]
        switch($letter){

            ({(($letter -eq "B") -or ($letter -eq "R"))}){
                #Write-host "letter: $letter ans x: $x"
                $length = $range.count
                $highest = $length-1
                $half = $length / 2
                $range = $range[$half..$highest]

            }
            ({(($letter -eq "F") -or ($letter -eq "L"))}){
                #Write-host "letter: $letter"
                $length = $range.count
                $half = $length / 2
                $lowest = $half -1
                $range = $range[0..$lowest]
            }
            default{
                Write-host "letter: $letter"
            }
        }
    }
    return $range
}

$listSeatID = @()

foreach($item in $data){
    $rowCode = $item.Substring(0,7)
    $columnCode = $item.Substring(7,3)
    $row = Calculate-Number -kind Row -code $rowCode
    $column = Calculate-Number -kind Column -code $ColumnCode
    $seatID = $row * 8 + $column

    $listSeatID += $seatID
    $listSeatID = $listSeatID | Sort-Object
    $missingID = $listSeatID[0]
    $x=0
    While(($($missingid+1) -eq $listSeatID[$x+1])){
        $x++
        $missingID = $listSeatID[$x]
    }
}

Write-host $($missingID+1)