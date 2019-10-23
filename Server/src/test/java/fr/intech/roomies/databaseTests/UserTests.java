package fr.intech.roomies.databaseTests;


import fr.intech.roomies.database.people.UserRepository;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.junit4.SpringRunner;

import static org.junit.Assert.*;

@RunWith(SpringRunner.class)
@SpringBootTest
@TestPropertySource(locations="classpath:application-test.properties")
public class UserTests {

    @Autowired
    private UserRepository userRepository;

    public void can_create_found_delete_a_user(){

        //assertEquals();
    }
}
