package fr.intech.roomies.model.grocery;

import javax.persistence.*;
import java.util.Date;
import java.util.List;

@Entity
public class GroceryList {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int id;

    @Column(nullable = false)
    private String Name;

    private Date date;

    @ElementCollection
    List<Item> items;

}
