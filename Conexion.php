<?php

    function ConexionBD(){
        $servername = "tcp:databasetarea1.ccdblu414uis.us-east-1.rds.amazonaws.com,1433";
        $connectionOptions = array("Database"=>"Tarea1BDI", 
        "Uid"=>"admin", "PWD"=>"bases5181");
        $conn = sqlsrv_connect($servername, $connectionOptions);
        if($conn == false)
            die(FormatErrors(sqlsrv_errors()));
            
        return $conn;
    }

?>


