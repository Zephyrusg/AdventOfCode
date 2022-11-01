$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay15"
#$data = Get-Content -Path "$($path)\Exampleday15"

class point{
    [int] $x
    [int] $y
    [int] $value
    [int] $distance
    [int] $estimatedDistance
    
    static [int]$width = 0
    static [int]$height= 0
    static [point [,]]$map = [System.Array]::CreateInstance( [point], 1,1)

    point([int]$y,[int]$x,[int]$value){
        $this.x = $x
        $this.y = $y
        $this.value = $value
        $this.distance = [int32]::MaxValue
        $this.estimatedDistance = [int32]::MaxValue

    }
    [boolean] Equals($otherPoint) {
        return (($this.x -eq $otherPoint.x) -and ($this.y -eq $otherPoint.y))
    }
    [collections.generic.list[point]] GetNeighbours() {
        $neighbourPoints = [collections.generic.list[point]]::new()
        if ($this.x -gt 0)                   {$neighbourPoints.Add($([point]::map[$this.y,$($this.x - 1) ]))}
        if ($this.x -lt [point]::width - 1)   {$neighbourPoints.Add($([point]::map[$this.y, $($this.x + 1)]))}
        if ($this.y -gt 0)                   {$neighbourPoints.Add($([point]::map[$($this.y - 1), $this.x]))}
        if ($this.y -lt [point]::height - 1)  {$neighbourPoints.Add($([point]::map[$($this.y + 1),$this.x ]))}
        return $neighbourPoints
    }
    [int] GetHashCode() {
        return $($this.y * [point]::width + $this.x)
    }
}

$smallHeight = $($data.Count)
$smallWidth = $($data[0].length)
[point]::height = $smallHeight * 5
[point]::width = $smallWidth * 5


[point]::map = [System.Array]::CreateInstance( [point], [point]::height,[point]::width)
$hNumber = 0

function PrintDistanceMap(){
    for ($y = 0; $y -lt $([point]::height); $y++) {
        $line = ""
        for ($x = 0; $x -lt $([point]::width); $x++) {
            $line += "$([point]::map[$y,$x].distance) ".PadLeft(3)
        }
        Write-Information $line -InformationAction Continue

    }
}

for ($bigY = 0; $bigY -lt 5; $bigY++)
{
    for ($bigX = 0; $bigX -lt 5; $bigX++)
    {
        for ($y = 0; $y -lt $smallHeight; $y++)
        {
            for ($x = 0; $x -lt $smallWidth; $x++)
            {   
                
                $datavalue = ([int]::parse($data[$y][$x]))
                $value = (($datavalue - 1 + $bigX + $bigY) % 9) + 1
                $xPos = ($bigX * $smallHeight) + $x;
                $yPos = ($bigY * $smallWidth) + $y;
                [Point]::map[$yPos, $xPos] = [Point]::new($yPos,$xPos,$value);
            }
        }
    }
}






[Point]$exitPoint = [Point]::map[$([Point]::Height - 1),$([Point]::Width - 1)]
$([Point]::map[0, 0]).distance = 0
$([Point]::map[0, 0]).estimatedDistance = 0
$openList = New-Object collections.generic.HashSet[point] 
$closedList = New-Object collections.generic.HashSet[point] 
$priorityList = [collections.generic.PriorityQueue[point, int]]::new()



$result = $openList.Add([point]$([point]::map[0,0]))
[point]$start = [point]::map[0,0]

$priorityList.Enqueue($start,$start.distance)
$endReached = $false
Write-Host "Start: $(Get-Date -Format "HH:mm")"
:loop while ($openList.Count > 0 && !$endReached)
{


    [Point]$current = $priorityList.Dequeue()
    if(($i % 1000) -eq 0){
        Write-host "$($current.x), $($current.y): $($openList.Count) $($closedList.Count)"
    }
    :neighbourloop foreach ($neighbourPoint in $current.GetNeighbours())
    {
        [int]$neighbourDistance = $current.distance + $neighbourPoint.value
        if ($neighbourPoint -eq $exitPoint)
        {
            Write-Host "End: $(Get-Date -Format "HH:mm")"
            write-host "Found output $neighbourDistance"
            break loop
        }
        [int]$estimatedNeighbourTotalDistance = $neighbourDistance# + ((([Point]::Width - $neighbourPoint.x) + ([Point]::Height - $neighbourPoint.y)) * $hNumber);
        if ($openList.Contains($neighbourPoint))
        {
            if ($estimatedNeighbourTotalDistance -lt $neighbourPoint.estimatedDistance)
            {
                $neighbourPoint.distance = $neighbourDistance
                $neighbourPoint.estimatedDistance = $estimatedNeighbourTotalDistance
                continue neighbourloop
            }
            else {
                continue neighbourloop
            }
        }
        if ($closedList.Contains($neighbourPoint))
        {
            if ($estimatedNeighbourTotalDistance -lt $neighbourPoint.estimatedDistance)
            {
                $result = $closedList.Remove($neighbourPoint);
            }
            else {
                continue neighbourloop
            }
        }
        $neighbourPoint.distance = $neighbourDistance;
        $neighbourPoint.estimatedDistance = $estimatedNeighbourTotalDistance;
        $result = $openList.Add($neighbourPoint) 
        $priorityList.Enqueue($neighbourPoint, $neighbourPoint.distance)
    }
    $result = $closedList.Add($current) 
    $result = $openList.Remove($current) 
    $i++
}