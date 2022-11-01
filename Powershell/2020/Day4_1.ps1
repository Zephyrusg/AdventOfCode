<#byr (Birth Year)
iyr (Issue Year)
eyr (Expiration Year)
hgt (Height)
hcl (Hair Color)
ecl (Eye Color)
pid (Passport ID)
cid (Country ID)#>

$data = Get-Content -Path .\InputDay4_1 -Raw

$nl = [System.Environment]::NewLine
$items = ($data -split "$nl$nl")

$words = @("byr";"iyr";"eyr";"hgt";"hcl";"ecl";"pid")

$validpassports = 0
$list = @()
foreach($item in $items){
    $valid = $true
    foreach($word in $words){
        if($item -notmatch $word){
            $valid = $false
        }
    }
    if($valid){
        $correct = $true
        $fields = ($item -split " ") -split "$nl"
        foreach($field in $fields){
            $info = $field -split ":"
            if($correct){    
                switch($info[0]){
                    "byr"{
                        if((($info[1] -lt 1920) -or ($info[1] -gt 2002)) -and ($info[1].Length -eq 4)) {
                            $correct = $false
                        }
                    }
                    "iyr"{
                        if((([int]$info[1] -lt 2010) -or ([int]$info[1] -gt 2020)) -and ($info[1].Length -eq 4)){
                            $correct = $false
                        }
                    }
                    "eyr"{
                        if((([int]$info[1] -lt 2020) -or ([int]$info[1] -gt 2030)) -and ($info[1].Length -eq 4)){
                            $correct = $false
                        }
                    }
                    "hgt"{
                        if($info[1] -match "cm"){
                            [int]$number = ($info[1]) -replace "[^0-9]" , ''
                            if(($number -lt 150) -or ($number -gt 193)){
                                $correct = $false
                            }
                        }elseif($info[1] -match "in"){
                            [int]$number = ($info[1]) -replace "[^0-9]" , ''
                            if(($number -lt 59) -or ($number -gt 76)){
                                $correct = $false
                            }
                        }else{
                            $correct = $false
                        }
                    }
                    "hcl"{
                        $pattern = '#[0-9a-f]{6}'
                        if($info[1] -notmatch $pattern){
                            $correct = $false
                        }
                    }
                    "ecl"{
                        $valideyecolors = @("amb";"blu";"brn";"gry";"grn";"hzl";"oth")
                        
                        foreach($valideyecolor in $valideyecolors){
                            if($info[1] -match $valideyecolor){
                                $correct = $true
                                break
                            }else{
                                $correct = $false
                            }
                        }
                    }
                    "pid"{
                        $pattern = '^[0-9]{9}$'
                        if($info[1] -notmatch $pattern){
                            $correct = $false
                        }
                    }
                }
            }
        }
        if($correct){
            $validpassports++
            $list += $fields
        }
    }
}
