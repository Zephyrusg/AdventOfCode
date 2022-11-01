using namespace System.Collections.Generic
$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday8"
$data = Get-Content -Path "$($path)\inputDay24"
$ErrorActionPreference = "stop"
[List[List[array]]]$instructionlist = @()

#done by hand, just a help to calculate everystep

class ALU{


    static [int]mul([int]$a, [int]$b ){
        return ($a *$b)
    }
    static [int]add([int]$a, [int]$b ){
        return ($a+$b)
    }
    static [int]div([int]$a, [int]$b ){
        if($b -le 0){
            return $a
        }
        
        return [math]::truncate($a/$b)
    }
    static [int]mod([int]$a, [int]$b ){
        if($b -le 0){
            return $a
        }
        return ($a % $b)
    }
    static [int]eql([int]$a, [int]$b ){
        if($a -eq $b){
            return 1
        
        }else{return 0}
        
    }

}

foreach($line in $data){

    if($line -match 'inp'){
        $list = New-Object list[array]
        $instruction = $line -split " "
        if($instruction[0] -ne "inp" -and $instruction[2] -notmatch '[xyzw]'){
            $instruction[2] = [int]::Parse($instruction[2])
        }
        $list.add($instruction)
    }elseif( $line -match '^\s*$'){
        $instructionlist.add($list)
    }else {
        $instruction = $line -split " "
        if($instruction[0] -ne "inp" -and $instruction[2] -notmatch '[xyzw]'){
            $instruction[2] = [int]::Parse($instruction[2])
        }
        $list.add($instruction)
    }

}
$instructionlist.add($list)

[int]$x = 0
[int]$y = 0
[int]$z = 0
[int]$w = 0

$modelnumber = [int[]]@(9,1,8,1,1,2,1,1,6,1,1,9,8,1)
$i = 0
foreach($part in $instructionlist){
    Write-Host "Step $($i+1)"
    foreach($instruction in $part){

        switch($instruction[0]){

            'inp'{
                set-Variable $instruction[1] -Value $modelnumber[$i]
            }
            'Add'{
                $a = (Get-Variable $instruction[1]).Value
                if($instruction[2] -match '[XYZW]'){
                    $b = (Get-Variable $instruction[2]).Value
                }else{
                    $b = $instruction[2]
                }
                set-Variable $instruction[1] -Value $([ALU]::add($a,$b))
            }
            'Mul'{
                $a = (Get-Variable $instruction[1]).Value
                if($instruction[2] -match '[XYZW]'){
                    $b = (Get-Variable $instruction[2]).Value
                }else{
                    $b = $instruction[2]
                }
                set-Variable $instruction[1] -Value $([ALU]::mul($a,$b))
            }
            'Div'{
                $a = (Get-Variable $instruction[1]).Value
                if($instruction[2] -match '[XYZW]'){
                    $b = (Get-Variable $instruction[2]).Value
                }else{
                    $b = $instruction[2]
                }
                set-Variable $instruction[1] -Value $([ALU]::div($a,$b))
            }
            'Mod'{
                $a = (Get-Variable $instruction[1]).Value
                if($instruction[2] -match '[XYZW]'){
                    $b = (Get-Variable $instruction[2]).Value
                }else{
                    $b = $instruction[2]
                }
                set-Variable $instruction[1] -Value $([ALU]::mod($a,$b))
            }
            'eql'{
                $a = (Get-Variable $instruction[1]).Value
                if($instruction[2] -match '[XYZW]'){
                    $b = (Get-Variable $instruction[2]).Value
                }else{
                    $b = $instruction[2]
                }
                set-Variable $instruction[1] -Value $([ALU]::eql($a,$b))
            }
        }

        Write-Host "$instruction `
         X:$x, Y:$y, Z:$z, W:$w  "


    }
    $i++

 }



















# W X=0 Y=0 Z=0


#Step 1
# inp w
# mul x 0  x -> 0
# add x z  x + z = x (0)
# mod x 26 x % 26 = # (0)
# div z 1  Z / 1  = Z (0)
# add x 12 X + 12 = X  (+12X) Negated
# eql x w  X(12) -EQ W[1-9] = X (0)
# eql x 0  X(0) -EQ 0 = X (X +1)  
# mul y 0  y * 0 = Y (0)
# add y 25 y + 25 y (+25 Y)
# mul y x  Y(25) *  X (1)= Y (25)  
# add y 1  Y(25) + 1 = Y (+1 / +26) 
# mul z y  z(0) * Y(26) = Z(0)
# mul y 0  Y * 0 = Y (0)
# add y w  Y + W [1-9] = Y+[1-9]
# add y 4  Y + 4 =  Y [1-9] + 4
# mul y x  Y * X(1) = Y [1-9] +4
# add z y  Z + Y = Z [1-9] + 4 

# X +1 Y (W1 + 4) Z (W1+4 )
# x = 1 Y = (W1+4) Z = (W1 +4)



# Step 2
# inp w
# mul x 0 X * 0 = X(0)
# add x z X + Z = x(W1+4)
# mod x 26 X % 26 = X (W1+4)
# div z 1  Z(W1+4)
# add x 15 X(W1+19)
# eql x w  X(W1+19) -eq  W[1-9] X(0)
# eql x 0  X(1)
# mul y 0  Y(0)
# add y 25 Y(25)
# mul y x  Y(25)
# add y 1  Y(26)
# mul z y   Z(26(W1+4))
# mul y 0   Y(0)
# add y w   Y(W2)
# add y 11  Y(W2+11)
# mul y x   Y(W2+11) 
# add z y   Z(26(W1+4)) + (W2+11)

#X = 1 Y = (W2+11) Z = Z(W2+11)(26(W1+4))

#step 3
# inp w
# mul x 0 x(0()
# add x z  X (W2+11)+(26(W1+4)) 
# mod x 26  x (W2+11)(26(W1+4)) % 26 
# div z 1   Z(W2+11)(26(W1+4))
# add x 11  x ((W2+11) % 26) + 11
# eql x w    x -eq W3 -> X (0)
# eql x 0    X(1)
# mul y 0    Y(0)
# add y 25   Y(24)
# mul y x    y(0)
# add y 1    y(1)
# mul z y    Z(W2+11)(26(W1+4))
# mul y 0    y(0)
# add y w    Y(w3)
# add y 7    Y(W3+7)
# mul y x    Y(w3+7)
# add z y    Z(W2+11)(26(W1+4)) + (w3+7)

# X = 1 Y = Y(w3+7) Z = (W2+11)(26(W1+4)) + (w3+7)

#step 4
# inp w
# mul x 0  X(0)
# add x z  X = (W2+11)(26(W1+4)) + (w3+7)
# mod x 26 X = (w3+7)
# div z 26 Z = (W2+11)(26(W1+4)) /26
# add x -14 X (w3+7)
# eql x w x(0)
# eql x 0 x(1)
# mul y 0  y(0)
# add y 25 y(25)
# mul y x y(25)
# add y 1 y(26)
# mul z y Z ((W2+11)(26(W1+4))
# mul y 0 Y(0)
# add y w y = [w4]
# add y 2 y = [w4+2]
# mul y x y [W4 +2]
# add z y Z = ((W2+11)(26(W1+4)) + (W4+2)

# X =1 Y = W4+2 Z = ((W2+11)(26(W1+4)) + (W4+2)  W1 9 W2 9 W3 9

#step 5
# inp w
# mul x 0 x(0) 
# add x z X ((W2+11)(26(W1+4)) + (W4+2)
# mod x 26 (W4+2)
# div z 1  ((W2+11)(26(W1+4)) + (W4+2)
# add x 12 (W4+14)
# eql x w  x(0)
# eql x 0  x(1)
# mul y 0  Y(0)
# add y 25 Y(25)
# mul y x y (25)
# add y 1   y(26)
# mul z y 26((W2+11)(26(W1+4))+(W4+2))
# mul y 0 y(0)
# add y w y W5
# add y 11  Y(W5+11)
# mul y x   Y(W5+11)
# add z y (26((W2+11)(26(W1+4))+(W4+2))) +(W5+11)

# X(1) Y = Y(W5+11) Z (26((W2+11)(26(W1+4))+(W4+2))) +(W5+11)

#Step 6
# inp w
# mul x 0
# add x z (26((W2+11)(26(W1+4))+(W4+2))) +(W5+11)
# mod x 26  (W5+11))
# div z 26   ((W2+11)(26(W1+4))+(W4+2)))
# add x -10 W5 + 1
# eql x w  X(0)
# eql x 0 X(1)
# mul y 0 Y(0)
# add y 25 y(25)
# mul y x  Y(25)
# add y 1  Y(26)
# mul z y  Z(26((W2+11)(26(W1+4))+(W4+2))))
# mul y 0  y(0)
# add y w  y(w6)
# add y 13  Y(W6 +13)
# mul y x   Y(W16+13)
# add z y  Z(26((W2+11)(26(W1+4))+(W4+2)))) + (W6+13)

#x(1) Y (W6+13) Z(26((W2+11)(26(W1+4))+(W4+2)))) + (W6+13)

#Step 7
# inp w
# mul x 0
# add x z  Z(26((W2+11)(26(W1+4))+(W4+2)))) + (W6+13)
# mod x 26  (W16+13)
# div z 1  Z(26((W2+11)(26(W1+4))+(W4+2)))) + (W6+13)
# add x 11  (W16+24)
# eql x w  
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y  26((26((W2+11)(26(W1+4))+(W4+2))))+(W6+13))
# mul y 0
# add y w
# add y 9  Y(W7 +9)
# mul y x
# add z y (26((26((W2+11)(26(W1+4))+(W4+2))))+(W6+13))) + Y(W7 +9)

#stap 8
# inp w
# mul x 0
# add x z
# mod x 26 X(W7 +9)
# div z 1
# add x 13 X(W7 +22)
# eql x w (0)
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y (26(26((26((W2+11)(26(W1+4))+(W4+2))))+(W6+13)))+Y(W7 +9))
# mul y 0
# add y w (w8+12)
# add y 12
# mul y x
# add z y


#stap 9
# inp w
# mul x 0
# add x z
# mod x -7 (w8+12)
# div z 26 (26((26((W2+11)(26(W1+4))+(W4+2))))+(W6+13)))+Y(W7 +9))
# eql x w
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y 26 ((26((26((W2+11)(26(W1+4))+(W4+2))))+(W6+13)))+Y(W7 +9)))
# mul y 0
# add y w
# add y 6 W9 +6
# mul y x
# add z y

#Stap 10
# inp w
# mul x 0
# add x z
# mod x 26
# div z 1 26(26((26((26((W2+11)(26(W1+4))+(W4+2))))+(W6+13)))+Y(W7 +9)))+W9+6)
# add x 10
# eql x w
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y
# mul y 0
# add y w W10+2
# add y 2
# mul y x
# add z y

#W9 = 

# inp w
# mul x 0
# add x z
# mod x 26
# div z 26
# add x -2 / W10+2 -2
# eql x w
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y
# mul y 0
# add y w
# add y 11 W11+11
# mul y x
# add z y

# inp w
# mul x 0
# add x z
# mod x 26
# div z 26
# add x -1
# eql x w
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y
# mul y 0
# add y w
# add y 12 W12+12
# mul y x
# add z y

# inp w
# mul x 0
# add x z
# mod x 26
# div z 26
# add x -4
# eql x w
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y
# mul y 0
# add y w
# add y 3 W13+3
# mul y x
# add z y

# inp w
# mul x 0
# add x z
# mod x 26 
# div z 26
# add x -12
# eql x w
# eql x 0
# mul y 0
# add y 25
# mul y x
# add y 1
# mul z y
# mul y 0
# add y w
# add y 13 W+14 +13
# mul y x X=0
# add z y Y=0




#13 []
#W14 [] W13+3 -12= 1 