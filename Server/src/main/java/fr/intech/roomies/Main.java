package fr.intech.roomies;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class Main {

    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
    }



//    @Bean(initMethod = "start", destroyMethod = "stop")
//    @ConditionalOnProperty(value = "h2.autoServer.enabled",havingValue = "true")
//    public Server inMemoryH2DatabaseaServer() throws SQLException {
//        return Server.createTcpServer(
//                "-tcp", "-tcpAllowOthers", "-tcpPort", "9090");
//    }

}