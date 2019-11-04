package fr.intech.roomies.database.people;

import fr.intech.roomies.model.people.Roomie;
import org.springframework.data.repository.CrudRepository;

public interface RoomieRepository extends CrudRepository<Roomie, Integer> {


    Roomie findRoomieByUserId(int userId);
}
