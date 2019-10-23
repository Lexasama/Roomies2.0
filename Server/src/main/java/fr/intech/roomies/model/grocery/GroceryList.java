package fr.intech.roomies.model.grocery;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import java.util.Date;
import java.util.List;

@Entity
public class GroceryList {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int id;

    private String Name;
    private Date date;
    private List<Item> items;

}
