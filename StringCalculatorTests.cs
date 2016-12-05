using System;
using NFluent;
using NUnit.Framework;

namespace StringCalculatorTest
{
    public class StringCalculatorTests
    {
        [Test]
        public void Should_return_zero_when_the_string_number_is_empty()
        {
            int actual = StringCalculator.Add(string.Empty);
            Check.That(actual).IsZero();
        }

        [TestCase("1",1)]
        [TestCase("2",2)]
        public void Should_return_plain_number_when_only_one_number_in_string(string numbers, int expected)
        {
            var actual = StringCalculator.Add(numbers);
            Check.That(actual).IsEqualTo(expected);
        }

        [Test]
        public void Should_return_sum_of_two_numbers_in_input_string()
        {
            var actual = StringCalculator.Add("1,2");
            Check.That(actual).IsEqualTo(3); 
        }

        [Test]
        public void Should_return_sum_of_several_numbers_in_input_string()
        {
            var actual = StringCalculator.Add("1,2,3");
            Check.That(actual).IsEqualTo(6);
        }

        [Test]
        public void Should_return_sum_when_the_delimiter_is_new_line()
        {
            var actual = StringCalculator.Add("1\n2,3");
            Check.That(actual).IsEqualTo(6);
        }

        [Test]
        public void Should_accept_custom_delimiter()
        {
            var actual = StringCalculator.Add("//;\n1;2");
            Check.That(actual).IsEqualTo(3);
        }

        [Test]
        public void Should_raise_an_exception_when_number_is_negative()
        {
            Check.ThatCode(() => StringCalculator.Add("-1")).Throws<Exception>().WithMessage("negatives not allowed: -1");
        }

        [Test]
        public void Should_specify_negative_numbers_in_error_message()
        {
            Check.ThatCode(() => StringCalculator.Add("-1,-2")).Throws<Exception>().WithMessage("negatives not allowed: -1,-2");
        }

        [Test]
        public void Should_ignore_number_when_it_is_bigger_than_1000()
        {
            Check.That(StringCalculator.Add("1001, 2")).IsEqualTo(2);
        }

        [Test]
        public void Should_allow_custom_delimiter_greater_than_one_character()
        {
            Check.That(StringCalculator.Add("//[***]\n1***2***3")).IsEqualTo(6);
        }

        [Test]
        public void Should_allow_multiple_custom_delimiters_greater_than_one_character()
        {
            Check.That(StringCalculator.Add("//[*][%]\n1*2%3")).IsEqualTo(6);
        }
    }
}
