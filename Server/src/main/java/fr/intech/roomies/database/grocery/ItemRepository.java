package fr.intech.roomies.database.grocery;

import fr.intech.roomies.model.grocery.Item;
import org.springframework.data.repository.CrudRepository;

public interface ItemRepository extends CrudRepository<Item, Integer> {
}
