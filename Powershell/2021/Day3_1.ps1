$location = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
$data = Get-Content -Path "$location\InputDay3_1"

[int]$lengthbiniarnumber = $data[0].Length

$columnCounter=0
$binairyNumber = ""
$maxBiniaryNumber = [math]::Pow(2,$lengthbiniarnumber) -1

for($i=0;$i -lt $lengthbiniarnumber; $i++){
    $ones = 0
    $zeros = 0

    foreach($row in $data){
        if($row[$columnCounter] -eq "0"){
            $zeros++        
        }else{
            $ones++
        }
    }

    if($zeros -gt $ones){
        $binairynumber += "0"
    }else{
        $binairynumber += "1"
    }
    $columnCounter++
}

[int]$gamma = [convert]::Toint32($binairynumber,2)

$e = $maxBiniaryNumber - $gamma

$anwser = $gamma * $e

<# $invertbinary = "111100100110"
[int]$e = [convert]::Toint32($invertbinary,2)

$binairynumber
$number = [Convert]::ToString([byte]$gamma,2).PadLeft($lengthbiniarnumber, '0')

$newnumber = $gamma -bxor 0xFFF
[Convert]::ToString($newnumber,2).PadLeft($lengthbiniarnumber, '0') #>