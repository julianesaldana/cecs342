import java.util.*;

public class Variance {
    static abstract class Animal {
        abstract String speak();
    }

    static class Frog extends Animal {
        String speak() {return "Woof";}
    }
    
    static class Cow extends Animal {
        String speak() {return "Woof";}
    }

    public static void printAnimals(Iterable<Animal> animals) {
        for (Animal a : animals) {
            System.out.println(a.speak());
        }
    }

    
    public static void addFrog(List<? super Frog> frogs) {
        frogs.add(new Frog());
    }
    public static void main(String[] args) {
        FrogEater yum = new FrogEater();
        yum.eat(new Frog());

        FrogEater yum2 = new AnimalEater();
        yum2.eat(new Frog());

        AnimalEater yum3 = new AnimalEater();
        yum3.eat(new Frog());
        yum3.eat(new Cow());
    }



   static abstract class AnimalFactory {
        abstract Animal makeAnimal();
    }

    static class FrogFactory extends AnimalFactory {
        Frog makeAnimal() {
            return new Frog();
        }
    }

    static interface FrogEater {
        void eat(Frog meal);
    }

    static class AnimalEater implements FrogEater {
        void eat(Animal meal) {
            // eat the Animal.
        }
    }





    
}

