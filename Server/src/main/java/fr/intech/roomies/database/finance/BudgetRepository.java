package fr.intech.roomies.database.finance;

import fr.intech.roomies.model.finance.Budget;
import org.springframework.data.repository.CrudRepository;

public interface BudgetRepository extends CrudRepository<Budget, Integer> {
}
