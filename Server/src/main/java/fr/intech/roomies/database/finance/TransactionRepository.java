package fr.intech.roomies.database.finance;

import fr.intech.roomies.model.finance.Transaction;
import org.springframework.data.repository.CrudRepository;

public interface TransactionRepository extends CrudRepository<Transaction, Integer> {
}
