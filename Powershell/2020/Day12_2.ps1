$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday12"
$data = Get-Content -Path "$($path)\inputDay12"

$VerbosePreference = "Continue"

class ship{
    [int[]]$location

    [hashtable]$waypoint

    ship([int[]]$location,[hashtable]$waypoint){
        $this.location = $location
        $this.waypoint = $waypoint
    }
    Rotate([int] $degrees){
        if($degrees -lt 0){
            $degrees = $degrees + 360
        }
        switch($degrees){
            90{
                $temp = $this.waypoint.y
                $this.waypoint.y = $this.waypoint.x
                $this.waypoint.x = $temp * -1
            }
            180{
                $this.waypoint.x *= -1
                $this.waypoint.y *= -1
            }
            270{
                $temp = $this.waypoint.x
                $this.waypoint.x = $this.waypoint.y
                $this.waypoint.y = $temp * -1
            }
        }
    }
    Foward([int] $number){
        $this.location[0] += $this.waypoint.x * $number
        $this.location[1] += $this.waypoint.y * $number
    }
    updateWaypoint([int]$number, [string]$direction){
        switch($direction){ 
        
            'N'{
                $this.waypoint.x += $number
            }
            'S' {
                $this.waypoint.x -= $number
            }   
            'W'{
                $this.waypoint.y -= $number
            }
            'E'{
                $this.waypoint.y += $number
            }
        }
    }

}

$startwaypoint =@{
    x =  1
    y = 10
}

[int[]]$startlocation = @(0,0)

$ship = [ship]::new($startlocation, $startwaypoint)

foreach($item in $data){

    $code = $item.Substring(0,1)
    [int]$number = ([int]::parse($($item.Substring(1))))

    switch($code){

        {$_ -eq 'L' -or $_ -eq 'R'}{
            
            if($code -eq 'L'){
                $number *= -1
            }
            $ship.Rotate($number)

        }
        'F'{
            $ship.Foward($number)
        }
        default{
            $ship.updateWaypoint($number, $code)
        }
    }
}

$location = $ship.location
$([Math]::abs($location[0])) + $([math]::Abs($location[1]))