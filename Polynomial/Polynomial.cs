using PolynomialObject.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace PolynomialObject
{
    public sealed class Polynomial
    {
        private readonly List<PolynomialMember> polynomial;

        public Polynomial()
        {
            polynomial = new List<PolynomialMember>();
        }

        public Polynomial(PolynomialMember member)
        {
            polynomial = new List<PolynomialMember>() { member };
        }

        public Polynomial(IEnumerable<PolynomialMember> members)
        {
            polynomial = members.ToList();
        }

        public Polynomial((double degree, double coefficient) member)
        {
            polynomial = new List<PolynomialMember>() { new PolynomialMember(member.degree, member.coefficient) };

        }

        public Polynomial(IEnumerable<(double degree, double coefficient)> members)
        {
            polynomial = new List<PolynomialMember>(members.Count());
            foreach (var (degree, coefficient) in members)
            {
                polynomial.Add(new PolynomialMember(degree, coefficient));
            }

        }

        /// <summary>
        /// The amount of not null polynomial members in polynomial 
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                for (int i = 0; i < polynomial.Count; i++)
                {
                    if (polynomial[i] != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// The biggest degree of polynomial member in polynomial
        /// </summary>
        public double Degree
        {
            get
            {
                double degree = 0;
                for (int i = 0; i < polynomial.Count; i++)
                {
                    if (polynomial[i] != null && polynomial[i].Degree >= degree)
                    {
                        degree = polynomial[i].Degree;
                    }
                }
                return degree;
            }
        }

        /// <summary>
        /// Adds new unique member to polynomial 
        /// </summary>
        /// <param name="member">The member to be added</param>
        /// <exception cref="PolynomialArgumentException">Throws when member to add with such degree already exist in polynomial</exception>
        /// <exception cref="PolynomialArgumentNullException">Throws when trying to member to add is null</exception>
        public void AddMember(PolynomialMember member)
        {
            if (member == null)
            {
                throw new PolynomialArgumentNullException();
            }
            if (member.Coefficient == 0)
            {
                throw new PolynomialArgumentException();
            }
            if (polynomial == null)
            {
                throw new PolynomialArgumentException();
            }
            for (int i = 0; i < polynomial.Count; i++)
            {
                if (polynomial[i] != null && polynomial[i].Degree == member.Degree)
                {
                    throw new PolynomialArgumentException();
                }
            }
            polynomial.Add(member);



        }

        /// <summary>
        /// Adds new unique member to polynomial from tuple
        /// </summary>
        /// <param name="member">The member to be added</param>
        /// <exception cref="PolynomialArgumentException">Throws when member to add with such degree already exist in polynomial</exception>
        public void AddMember((double degree, double coefficient) member)
        {


            if (polynomial == null)
            {
                throw new PolynomialArgumentException();
            }
            if ((member.coefficient == 0 && member.degree == 0) || member.coefficient == 0)
            {
                throw new PolynomialArgumentException();
            }


            for (int i = 0; i < polynomial.Count; i++)
            {
                if (polynomial[i] != null && polynomial[i].Degree == member.degree)
                {
                    throw new PolynomialArgumentException();
                }
            }
            polynomial.Add(new PolynomialMember(member.degree, member.coefficient));
        }

        /// <summary>
        /// Removes member of specified degree
        /// </summary>
        /// <param name="degree">The degree of member to be deleted</param>
        /// <returns>True if member has been deleted</returns>
        public bool RemoveMember(double degree)
        {
            if (polynomial != null)
            {
                for (int i = 0; i < polynomial.Count; i++)
                {
                    if (polynomial[i] != null && polynomial[i].Degree == degree)
                    {
                        polynomial.Remove(polynomial[i]);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Searches the polynomial for a method of specified degree
        /// </summary>
        /// <param name="degree">Degree of member</param>
        /// <returns>True if polynomial contains member</returns>
        public bool ContainsMember(double degree)
        {
            if (polynomial != null)
            {
                for (int i = 0; i < polynomial.Count; i++)
                {
                    if (polynomial[i] != null && polynomial[i].Degree == degree)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Finds member of specified degree
        /// </summary>
        /// <param name="degree">Degree of member</param>
        /// <returns>Returns the found member or null</returns>
        public PolynomialMember Find(double degree)
        {
            if (polynomial != null)
            {
                for (int i = 0; i < polynomial.Count; i++)
                {
                    if (polynomial[i] != null && polynomial[i].Degree == degree)
                    {
                        return polynomial[i];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets and sets the coefficient of member with provided degree
        /// If there is no null member for searched degree - return 0 for get and add new member for set
        /// </summary>
        /// <param name="degree">The degree of searched member</param>
        /// <returns>Coefficient of found member</returns>
        public double this[double degree]
        {
            get
            {
                if (polynomial != null)
                {
                    for (int i = 0; i < polynomial.Count; i++)
                    {
                        if (polynomial[i] != null && polynomial[i].Degree == degree)
                        {
                            return polynomial[i].Coefficient;
                        }
                    }
                }
                return 0;
            }
            set
            {
                if (value != 0)
                {
                    if (polynomial != null)
                    {
                        for (int i = 0; i < polynomial.Count; i++)
                        {
                            if (polynomial[i] != null && polynomial[i].Degree == degree)
                            {
                                polynomial[i].Coefficient = value;
                                return;
                            }
                        }
                        this.AddMember((degree, value));

                    }
                }
                else
                {
                    for (int i = 0; i < polynomial.Count; i++)
                    {
                        if (polynomial[i] != null && polynomial[i].Degree == degree)
                        {
                            polynomial.Remove(polynomial[i]);
                        }
                    }
                }



            }
        }

        /// <summary>
        /// Convert polynomial to array of included polynomial members 
        /// </summary>
        /// <returns>Array with not null polynomial members</returns>
        public PolynomialMember[] ToArray()
        {
            return polynomial.ToArray();
        }

        /// <summary>
        /// Adds two polynomials
        /// </summary>
        /// <param name="a">The first polynomial</param>
        /// <param name="b">The second polynomial</param>
        /// <returns>New polynomial after adding</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if either of provided polynomials is null</exception>
        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial result = new Polynomial();
            foreach (var el in b.polynomial)
            {
                result.Add((el.Degree, el.Coefficient));
            }
            foreach (var el in a.polynomial)
            {
                result.Add((el.Degree, el.Coefficient));
            }

            return result;
        }

        /// <summary>
        /// Subtracts two polynomials
        /// </summary>
        /// <param name="a">The first polynomial</param>
        /// <param name="b">The second polynomial</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if either of provided polynomials is null</exception>
        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial result = new Polynomial();
            foreach (var el in b.polynomial)
            {
                result.Subtraction((el.Degree, el.Coefficient));
            }
            foreach (var el in a.polynomial)
            {
                result.Subtraction((el.Degree, el.Coefficient));
            }

            return result;
        }

        /// <summary>
        /// Multiplies two polynomials
        /// </summary>
        /// <param name="a">The first polynomial</param>
        /// <param name="b">The second polynomial</param>
        /// <returns>Returns new polynomial after multiplication</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if either of provided polynomials is null</exception>
        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial result = new Polynomial();
            foreach (var el in a.polynomial)
            {
                result = result + b.Multiply((el.Degree, el.Coefficient));
            }
            return result;
        }

        /// <summary>
        /// Adds polynomial to polynomial
        /// </summary>
        /// <param name="polynomial">The polynomial to add</param>
        /// <returns>Returns new polynomial after adding</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if provided polynomial is null</exception>
        public Polynomial Add(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new PolynomialArgumentNullException();
            }
            return this + polynomial;
        }

        /// <summary>
        /// Subtracts polynomial from polynomial
        /// </summary>
        /// <param name="polynomial">The polynomial to subtract</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if provided polynomial is null</exception>
        public Polynomial Subtraction(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new PolynomialArgumentNullException();
            }
            return this - polynomial;
        }

        /// <summary>
        /// Multiplies polynomial with polynomial
        /// </summary>
        /// <param name="polynomial">The polynomial for multiplication </param>
        /// <returns>Returns new polynomial after multiplication</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if provided polynomial is null</exception>
        public Polynomial Multiply(Polynomial polynomial)
        {
            return this * polynomial;
        }

        /// <summary>
        /// Adds polynomial and tuple
        /// </summary>
        /// <param name="a">The polynomial</param>
        /// <param name="b">The tuple</param>
        /// <returns>Returns new polynomial after adding</returns>
        public static Polynomial operator +(Polynomial a, (double degree, double coefficient) b)
        {
            if (a == null)
            {
                throw new PolynomialArgumentNullException();
            }
            if (b.coefficient == 0)
            {
                return a;

            }
            Polynomial result = a;
            if (a[b.degree] == 0)
            {
                result.AddMember(new PolynomialMember(b.degree, b.coefficient));
            }
            else
            {
                a[b.degree] += b.coefficient;
                if (a[b.degree] == 0)
                {
                    a.RemoveMember(b.degree);
                }
            }

            return result;
        }

        /// <summary>
        /// Subtract polynomial and tuple
        /// </summary>
        /// <param name="a">The polynomial</param>
        /// <param name="b">The tuple</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        public static Polynomial operator -(Polynomial a, (double degree, double coefficient) b)
        {
            if (a == null)
            {
                throw new PolynomialArgumentNullException();
            }
            if (b.coefficient == 0)
            {
                return a;

            }
            Polynomial result = a;
            if (a[b.degree] == 0)
            {
                result.AddMember(new PolynomialMember(b.degree, 0 - b.coefficient));
            }
            else
            {
                a[b.degree] -= b.coefficient;
                if (a[b.degree] == 0)
                {
                    a.RemoveMember(b.degree);
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies polynomial and tuple
        /// </summary>
        /// <param name="a">The polynomial</param>
        /// <param name="b">The tuple</param>
        /// <returns>Returns new polynomial after multiplication</returns>
        public static Polynomial operator *(Polynomial a, (double degree, double coefficient) b)
        {
            if (a == null)
            {
                throw new PolynomialArgumentNullException();
            }
            if (b.coefficient == 0)
            {
                return new Polynomial();

            }
            Polynomial result = a;

            foreach (var el in result.polynomial)
            {
                el.Coefficient *= b.coefficient;
                el.Degree += b.degree;
            }


            return result;
        }

        /// <summary>
        /// Adds tuple to polynomial
        /// </summary>
        /// <param name="member">The tuple to add</param>
        /// <returns>Returns new polynomial after adding</returns>
        public Polynomial Add((double degree, double coefficient) member)
        {

            Polynomial result = this;


            if (member.coefficient != 0)
            {
                if (result[member.degree] == 0)
                {
                    result.AddMember(new PolynomialMember(member.degree, member.coefficient));
                }
                else
                {
                    result[member.degree] += member.coefficient;
                    if (result[member.degree] == 0)
                    {
                        result.RemoveMember(member.degree);
                    }
                }

            }


            return result;
        }

        /// <summary>
        /// Subtracts tuple from polynomial
        /// </summary>
        /// <param name="member">The tuple to subtract</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        public Polynomial Subtraction((double degree, double coefficient) member)
        {
            Polynomial result = new Polynomial(this.polynomial);

            if (member.coefficient != 0)
            {
                if (result[member.degree] == 0)
                {
                    result.AddMember(new PolynomialMember(member.degree, 0 - member.coefficient));
                }
                else
                {
                    result[member.degree] = result[member.degree] - member.coefficient;
                    if (result[member.degree] == 0)
                    {
                        result.RemoveMember(member.degree);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies tuple with polynomial
        /// </summary>
        /// <param name="member">The tuple for multiplication </param>
        /// <returns>Returns new polynomial after multiplication</returns>
        public Polynomial Multiply((double degree, double coefficient) member)
        {
            Polynomial result = this;
            if (polynomial == null)
            {
                return new Polynomial();
            }
            if (member.coefficient != 0)
                result = this * member;
            else
                result = new Polynomial();
            return result;
        }
    }
}
