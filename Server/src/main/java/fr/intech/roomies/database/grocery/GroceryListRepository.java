package fr.intech.roomies.database.grocery;

import fr.intech.roomies.model.grocery.GroceryList;
import org.springframework.data.repository.CrudRepository;

public interface GroceryListRepository extends CrudRepository<GroceryList, Integer> {
}
