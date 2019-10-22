package fr.intech.roomies.database;

import fr.intech.roomies.model.people.User;
import org.springframework.data.repository.CrudRepository;

public interface UserRepository extends CrudRepository<User, Integer> {
}
