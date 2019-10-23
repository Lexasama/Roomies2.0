package fr.intech.roomies.database.people;

import fr.intech.roomies.model.people.Supervisor;
import org.springframework.data.repository.CrudRepository;

import java.util.List;

public interface SupervisorRepository extends CrudRepository<Supervisor, Integer> {

    Supervisor findSupervisorById(int userId);

    List<Supervisor> findSupervisorByLastName( String lastName);

}
