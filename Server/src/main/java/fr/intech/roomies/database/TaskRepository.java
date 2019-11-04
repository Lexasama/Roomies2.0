package fr.intech.roomies.database;

import fr.intech.roomies.model.Task;
import org.springframework.data.repository.CrudRepository;

public interface TaskRepository extends CrudRepository<Task, Integer> {
}
