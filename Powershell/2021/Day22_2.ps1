using namespace System.Numerics
using namespace System.Collections.Generic

$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay22"
#$data = Get-Content -Path "$($path)\Exampleday22"

class cube{
    [vector3]$downLeft 
    [vector3]$upRight
    
    cube([vector3]$upRight,[vector3]$downLeft){
        $this.upRight = $upRight
        $this.downLeft = $downLeft

    }
    [boolean] ValidCube(){

        Return [bool](($this.downLeft.X -le $this.upRight.X) -and ($this.downLeft.Y -le $this.upRight.Y) -and ($this.downLeft.Z -le $this.upRight.Z))

    }

    [long] Width(){
        return $($this.upright.X - $this.downLeft.X + 1)
    }

    [long] Height(){
        return $($this.upRight.Y - $this.downLeft.Y + 1)
    }

    [long] Depth(){
        return $($this.upRight.Z - $this.downLeft.Z + 1)
    }

    [long] Volume(){
        return $($this.Width() * $this.Height() * $this.Depth())
    }

    static [cube] Intersection([cube]$one, [cube]$two){
        $uprightIntersect = [Vector3]::new($([math]::Min($one.upRight.X, $two.upRight.X)),$([math]::Min($one.upRight.Y, $two.upRight.Y)),$([math]::Min($one.upRight.Z, $two.upRight.Z)))
        $downLeftIntersect = [Vector3]::new($([math]::Max($one.downLeft.X, $two.downLeft.X)),$([math]::Max($one.downLeft.Y, $two.downLeft.Y)),$([math]::Max($one.downLeft.Z, $two.downLeft.Z)))
        return $([Cube]::new($uprightIntersect,$downLeftIntersect))
    }

    static [cube[]] SplitCube([cube]$one, [cube]$two){
        $intersection = [cube]::intersection($one,$two)
        if($intersection.validcube()){
            $splitCubes =  [list[cube]]@()
            if($intersection.downleft.X -gt $one.downLeft.X){
                $newDownLeft = [vector3]::new($($one.downLeft.X),$($one.downLeft.Y),$($one.downLeft.Z))
                $newUpRight = [Vector3]::new($($intersection.downLeft.X - 1),$($one.upright.Y),$($one.upRight.Z))
                $splitCubes.Add([Cube]::new($newupright,$newdownLeft))
            }   
            if ($intersection.upRight.X -lt $one.upRight.X){
                $newDownLeft = [vector3]::new($($intersection.upright.X + 1),$($one.downLeft.Y),$($one.downLeft.z))
                $newUpRight = [Vector3]::new($($one.upright.X),$($one.upright.Y),$($one.upRight.Z))
                $splitCubes.Add([Cube]::new($newupright,$newdownLeft))
            }
            if ($intersection.downLeft.Y -gt $one.downLeft.Y){
                $newDownLeft = [vector3]::new($($intersection.downLeft.X),$($one.downLeft.Y),$($one.downLeft.z))
                $newUpRight = [Vector3]::new($($intersection.upright.X),$($intersection.downLeft.Y - 1),$($one.upRight.Z))
                $splitCubes.Add([Cube]::new($newupright,$newdownLeft))
            }
            if ($intersection.upRight.Y -lt $one.upRight.Y){
                $newDownLeft = [vector3]::new($($intersection.downLeft.X),$($intersection.upRight.Y + 1),$($one.downLeft.z))
                $newUpRight = [Vector3]::new($($intersection.upright.X),$($one.upRight.Y),$($one.upRight.Z))
                $splitCubes.Add([Cube]::new($newupright,$newdownLeft))
            }
            if ($intersection.downLeft.Z -gt $one.downLeft.Z){
                $newDownLeft = [vector3]::new($($intersection.downLeft.X),$($intersection.downLeft.Y),$($one.downLeft.z))
                $newUpRight = [Vector3]::new($($intersection.upright.X),$($intersection.upRight.Y),$($intersection.downLeft.Z - 1))
                $splitCubes.Add([Cube]::new($newupright,$newdownLeft))
            }
            if ($intersection.upRight.Z -lt $one.upRight.Z){   
                $newDownLeft = [vector3]::new($($intersection.downLeft.X),$($intersection.downLeft.Y),$($intersection.upRight.Z + 1))
                $newUpRight = [Vector3]::new($($intersection.upright.X),$($intersection.upRight.Y),$($one.upRight.Z))
                $splitCubes.Add([Cube]::new($newupright,$newdownLeft))
            }
            return $splitCubes.ToArray()
        }else{
            return [Cube[]]@($one)
        } 
    }

}


$cubelist = [List[Cube]]@()
$tempCubeList = [List[Cube]]@()
$linecounter = 1
foreach($line in $data){
    write-host "Starting with $linecounter / $($data.count)"
    $pattern = '-?\d{1,6}..-?\d{1,6}'
    #$Line -match $pattern

    [bool]$turnon = $Line -match "on"
    $line = $line -replace 'on ',"" -replace 'off ',""
    $parts = $line -split ','

    $null = $parts[0] -match $pattern
    $Xpart = $Matches[0]
    $Xpart = $Xpart -split "\.."
    $xMin = [int]::Parse($Xpart[0])
    $xMax = [int]::Parse($Xpart[1])

    $null = $parts[1] -match $pattern
    $Ypart = $Matches[0]
    $Ypart = $Ypart -split "\.."
    $yMin = [int]::Parse($Ypart[0])
    $yMax = [int]::Parse($Ypart[1])

    $null = $parts[2] -match $pattern
    $zpart = $Matches[0]
    $Zpart = $Zpart -split "\.."
    $zMin = [int]::Parse($Zpart[0])
    $zMax = [int]::Parse($Zpart[1])

    $upright = [Vector3]::new($xMax,$yMax,$zMax)
    $downLeft = [vector3]::new($xMin,$ymin,$zMin)

    $cube = [cube]::new($upright,$downLeft)
    for($i = 0;$i -lt $Cubelist.Count;$i++){
        $tempCubeList.AddRange([cube]::SplitCube($cubelist[$i],$cube))
    }
    if($turnon){
        $tempCubeList.Add($cube)
    }
    ($cubelist, $tempCubeList) = ($tempCubeList,$cubelist)
    $tempCubeList.Clear()


    $linecounter ++
}

[bigint]$answer = 0
for($i=0;$i -lt $cubelist.Count;$i++){
    $answer += $cubelist[$i].Volume()
}

$answer

# $turnedonPoints.Count