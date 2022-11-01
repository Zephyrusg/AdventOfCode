$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay16"
#$data = Get-Content -Path "$($path)\Exampleday16_1"

$global:pkgs = New-Object Collections.Generic.List[PSCustomObject]
function ConvertTo-Biniar{
    param(
        [String]
        $dataString
    )
    $hashBinair = @{
        '0' = "0000"
        '1' = "0001"
        '2' = "0010"
        '3' = "0011"
        '4' = "0100"
        '5' = "0101"
        '6' = "0110"
        '7' = "0111"
        '8' = "1000"
        '9' = "1001"
        'A' = "1010"
        'B' = "1011"
        'C' = "1100"
        'D' = "1101"
        'E' = "1110"
        'F' = "1111"
    }

    $StringBiniar = ""
    for($i=0;$i -lt $dataString.Length;$i++){
        $StringBiniar +=  $hashBinair[[string]$($datastring[$i])]
    }

    return $StringBiniar
}

Function ConvertFrom-LiteralBinary{
    param(
        $stringBinair
    )
    $length = 0
    $set = ""

    do{
        $subset = $stringBinair.Substring($length,5)
        $length += 5
        $set += $subset.Substring(1)

    }while($subset[0] -eq '1')
    
    $literal = [convert]::Toint64($set,2)
    
    return @($literal,$length)
}

Function ConvertFrom-Operator{
    param(
        $stringBinair
    )

    $lengthID = $stringBinair[0]
    [long]$value = 0

    switch($lengthID){
        '0'{
            $lengthBiniar = $stringBinair.Substring(1,15)
            $length = [convert]::Toint64($lengthBiniar,2)
            #$value += ConvertFrom-LiteralBinary -stringBinair $stringBinair
            $totalLength = 16
            while($totalLength -ne $length + 16){
              $Pkg = New-AoCPackage -stringBinair $stringBinair.Substring($totalLength)
              $totalLength += $pkg.length

            }
        }
        '1'{
            $countBiniar = $stringBinair.Substring(1,11)
            $count = [convert]::Toint64($countBiniar,2)
            $totalLength = 12
            for($i=0;$i -lt $count;$i++){
                $Pkg = New-AoCPackage -stringBinair $stringBinair.Substring($totalLength)
                $totalLength += $pkg.Length
            }
        }
    }

    return @($value, $totalLength)

}

Function New-AoCPackage{
    param(
        [string]
        $stringBinair
    )
    
    $versionBinair =  $stringBinair.Substring(0,3)
    $version = [convert]::Toint32($versionBinair,2)

    $typeBinair = $stringBinair.Substring(3,3)
    $type = [convert]::Toint32($typeBinair,2)
    
    if($type -eq 4){
        $returnArray = Convertfrom-LiteralBinary -stringBinair $stringBinair.Substring(6)
    }else{
        $returnArray  = ConvertFrom-Operator -stringBinair $stringBinair.Substring(6)
    }

    $obj = [pscustomobject]@{
        version = $version
        type    = $type
        value   = $returnArray[0]
        length  = $returnArray[1] + 6
    }
    $global:pkgs.add($obj)
    return $obj
}


$stringBinair = ConvertTo-Biniar -dataString $data
New-AoCPackage -stringBinair $stringBinair
($pkgs | Measure-Object -Property version -Sum).sum