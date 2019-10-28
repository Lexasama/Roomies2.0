package fr.intech.roomies.model.grocery;

import fr.intech.roomies.model.people.Roomie;

import javax.persistence.*;
import java.util.List;

@Entity
public class Item {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int itemId;

    @Column(nullable = false)
    private String name;

    @Column(nullable = false)
    private double price;

    private int quantity;
}
