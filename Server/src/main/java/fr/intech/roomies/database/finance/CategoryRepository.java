package fr.intech.roomies.database.finance;

import fr.intech.roomies.model.finance.Category;
import org.springframework.data.repository.CrudRepository;

public interface CategoryRepository extends CrudRepository<Category, Integer> {
}
