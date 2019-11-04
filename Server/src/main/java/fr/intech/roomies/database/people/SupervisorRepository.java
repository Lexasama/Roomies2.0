package fr.intech.roomies.database.people;

import fr.intech.roomies.model.people.Supervisor;
import org.springframework.data.repository.CrudRepository;



public interface SupervisorRepository extends CrudRepository<Supervisor, Integer> {

    Supervisor findSupervisorByUserId(int userId);


}
