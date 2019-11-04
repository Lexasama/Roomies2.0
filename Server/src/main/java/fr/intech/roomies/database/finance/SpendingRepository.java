package fr.intech.roomies.database.finance;

        import fr.intech.roomies.model.finance.Spending;
        import org.springframework.data.repository.CrudRepository;

public interface SpendingRepository extends CrudRepository<Spending, Integer> {
}
