package fr.intech.roomies.database;

import org.springframework.data.repository.CrudRepository;

public interface UserRepository extends CrudRepository<User, int> {
}
